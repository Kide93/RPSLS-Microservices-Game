using ChoiceService.Business.Contracts;
using ChoiceService.Business.Handlers;
using ChoiceService.Business.Models;
using ChoiceService.Business.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChoiceService.Tests.Integration
{
    public class RandomChoiceHandlerTests
    {
        private readonly IMediator _mediator;
        private readonly Mock<IRandomNumberService> _randomNumberServiceMock;
        private readonly Mock<ILogger<RandomChoiceHandler>> _loggerMock;
        private readonly ServiceProvider _serviceProvider;

        public RandomChoiceHandlerTests()
        {
            _randomNumberServiceMock = new Mock<IRandomNumberService>();
            _loggerMock = new Mock<ILogger<RandomChoiceHandler>>();

            var services = new ServiceCollection();

            services.AddMediatR(typeof(RandomChoiceHandler.CommandHandler).Assembly);

            services.AddSingleton(_randomNumberServiceMock.Object);
            services.AddSingleton(_loggerMock.Object);

            _serviceProvider = services.BuildServiceProvider();
            _mediator = _serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldReturnRock_WhenRandomNumberIsWithinRangeForRock()
        {
            // Arrange
            _randomNumberServiceMock.Setup(x => x.GetRandomNumberAsync())
                .ReturnsAsync(10);

            var request = new GetRandomChoiceRequest();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Id);
            Assert.Equal("Rock", response.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnSpock_WhenRandomNumberIsOutOfRange()
        {
            // Arrange
            _randomNumberServiceMock.Setup(x => x.GetRandomNumberAsync())
                .ReturnsAsync(150);

            var request = new GetRandomChoiceRequest();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)ChoiceEnum.Spock, response.Id);
            Assert.Equal("Spock", response.Name);

            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Random number 150 is out of the expected range")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnPaper_WhenRandomNumberIsWithinRangeForPaper()
        {
            // Arrange
            _randomNumberServiceMock.Setup(x => x.GetRandomNumberAsync())
                .ReturnsAsync(35);

            var request = new GetRandomChoiceRequest();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)ChoiceEnum.Paper, response.Id);
            Assert.Equal("Paper", response.Name);
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
