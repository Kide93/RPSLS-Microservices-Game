using GameStatsService.Presentation.Contracts;
using GameStatsService.Presentation.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RabbitMqMessageConsumer>();

builder.Services.AddSingleton<IScoreboardService, ScoreboardService>();
builder.Services.AddSingleton<RabbitMqMessageConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var consumer = app.Services.GetRequiredService<RabbitMqMessageConsumer>();
consumer.StartListening();

app.Run();
