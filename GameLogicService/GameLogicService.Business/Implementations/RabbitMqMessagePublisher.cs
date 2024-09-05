using GameLogicService.Business.Contracts;
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

        public RabbitMqMessagePublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "gameResults",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public Task PublishGameResultEvent(GameResultEvent gameResultEvent)
        {
            var json = JsonSerializer.Serialize(gameResultEvent);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                routingKey: "gameResults",
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
    }
}
