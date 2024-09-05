using GameStatsService.Presentation.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Events;
using System.Text;
using System.Text.Json;

namespace GameStatsService.Presentation.Implementations
{

    public class RabbitMqMessageConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IScoreboardService _scoreboardService;
        private readonly ILogger<RabbitMqMessageConsumer> _logger;

        public RabbitMqMessageConsumer(IScoreboardService scoreboardService, ILogger<RabbitMqMessageConsumer> logger)
        {
            _scoreboardService = scoreboardService;
            _logger = logger;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "gameResults",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var gameResultEvent = JsonSerializer.Deserialize<GameResultEvent>(message);

                _logger.LogInformation($"Message received: {message}");

                await _scoreboardService.UpdateScoreboard(gameResultEvent);
            };

            _channel.BasicConsume(queue: "gameResults",
                autoAck: true,
                consumer: consumer);
        }
    }
}
