using GameLogicService.Business.Contracts;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Shared.Events;
using System.Text;
using System.Text.Json;

namespace GameLogicService.Business.Implementations
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqMessagePublisher> _logger;

        public RabbitMqMessagePublisher(ILogger<RabbitMqMessagePublisher> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "gameResults",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public async Task PublishGameResultEvent(GameResultEvent gameResultEvent)
        {
            try
            {
                var json = JsonSerializer.Serialize(gameResultEvent);
                var body = Encoding.UTF8.GetBytes(json);

                _channel.BasicPublish(exchange: "",
                    routingKey: "gameResults",
                    basicProperties: null,
                    body: body);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error publishing message: {ex.Message}");
            }
        }
    }
}
