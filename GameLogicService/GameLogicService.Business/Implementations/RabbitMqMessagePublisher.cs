using GameLogicService.Business.Contracts;
using RabbitMQ.Client;
using Shared.Events;
using Shared.Exceptions;
using System.Net;
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
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error publishing message: {ex.Message}");
                throw new ExternalServiceException("Failed to publish game result event", ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
