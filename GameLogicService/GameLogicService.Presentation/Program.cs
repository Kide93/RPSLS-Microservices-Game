using FluentValidation;
using GameLogicService.Business.Contracts;
using GameLogicService.Business.Implementations;
using GameLogicService.Business.Settings;
using GameLogicService.Business.Validators;
using GameLogicService.Presentation;
using MediatR;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<ExternalApiSettings>(
    builder.Configuration.GetSection("ExternalApiSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ExternalApiSettings:ChoiceServiceApiUrl"]);
}).AddPolicyHandler(GetRetryPolicy());

builder.Services.AddTransient<IChoiceStateFactory, ChoiceStateFactory>();

builder.Services.AddMediatR(GameLogicService.Business.AssemblyReference.Reference);

builder.Services.AddValidatorsFromAssembly(GameLogicService.Business.AssemblyReference.Reference);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (result, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Request failed. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
            });
}