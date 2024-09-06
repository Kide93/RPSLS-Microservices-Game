using FluentValidation;
using GameStatsService.Business;
using GameStatsService.Business.Validators;
using GameStatsService.Infrastructure;
using GameStatsService.Infrastructure.Repository;
using GameStatsService.Presentation.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSingleton<RabbitMqMessageConsumer>();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameStatsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameStatsDb")));

builder.Services.AddMediatR(AssemblyReference.Reference);
builder.Services.AddValidatorsFromAssembly(AssemblyReference.Reference);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStatsDbContext>();
    dbContext.Database.Migrate();
    dbContext.Initialize();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var consumer = app.Services.GetRequiredService<RabbitMqMessageConsumer>();
consumer.StartListening();

app.Run();
