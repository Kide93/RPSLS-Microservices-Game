using GameStatsService.Infrastructure;
using GameStatsService.Presentation.Contracts;
using GameStatsService.Presentation.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RabbitMqMessageConsumer>();
builder.Services.AddSingleton<IScoreboardService, ScoreboardService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameStatsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameStatsDb")));

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var consumer = app.Services.GetRequiredService<RabbitMqMessageConsumer>();
consumer.StartListening();

app.Run();
