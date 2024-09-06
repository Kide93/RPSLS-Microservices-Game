using GameStatsService.Business.Requests;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GameStatsService.Presentation.Implementations
{

    public class RabbitMqMessageConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqMessageConsumer> _logger;
        private readonly IMediator _mediator;

        public RabbitMqMessageConsumer(
            ILogger<RabbitMqMessageConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
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
                var gameResultEvent = JsonSerializer.Deserialize<GameResultRequest>(message);

                _logger.LogInformation($"Message received: {message}");

                _mediator.Send(gameResultEvent);
            };

            _channel.BasicConsume(queue: "gameResults",
                autoAck: true,
                consumer: consumer);
        }
    }
}
