using GameLogicService.Business.Contracts;
using GameLogicService.Business.Handlers;
using GameLogicService.Business.Requests;
using Moq;
using Shared.Enums;
using Shared.Events;
using Shouldly;

namespace GameLogicService.Tests.Integration
{
    public class PlayerChoiceHandlerTests
    {
        private readonly Mock<IChoiceStateFactory> _choiceStateFactoryMock;
        private readonly Mock<IExternalApiService> _externalApiServiceMock;
        private readonly Mock<IMessagePublisher> _messagePublisherMock;
        private readonly PlayerChoiceHandler.CommandHandler _commandHandler;

        public PlayerChoiceHandlerTests()
        {
            _choiceStateFactoryMock = new Mock<IChoiceStateFactory>();
            _externalApiServiceMock = new Mock<IExternalApiService>();
            _messagePublisherMock = new Mock<IMessagePublisher>();

            _commandHandler = new PlayerChoiceHandler.CommandHandler(
                _choiceStateFactoryMock.Object,
                _externalApiServiceMock.Object,
                _messagePublisherMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnGameResultResponse_WithCorrectOutcome()
        {
            // Arrange
            var request = new PlayerChoiceRequest
            {
                UserId = "validUserId",
                Choice = ChoiceEnum.Rock
            };

            var computerChoice = ChoiceEnum.Scissors;
            var playerStateMock = new Mock<IChoiceState>();
            var computerStateMock = new Mock<IChoiceState>();

            playerStateMock.Setup(p => p.CalculateResult(It.IsAny<IChoiceState>()))
                .Returns(GameOutcomeEnum.Win);

            _externalApiServiceMock
                .Setup(service => service.GetRandomChoiceAsync())
                .ReturnsAsync(computerChoice);

            _choiceStateFactoryMock
                .Setup(factory => factory.GetStateForChoice(ChoiceEnum.Rock))
                .Returns(playerStateMock.Object);

            _choiceStateFactoryMock
                .Setup(factory => factory.GetStateForChoice(computerChoice))
                .Returns(computerStateMock.Object);

            // Act
            var response = await _commandHandler.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldNotBeNull();
            response.PlayerChoice.ShouldBe((int)ChoiceEnum.Rock);
            response.ComputerChoice.ShouldBe((int)computerChoice);
            response.Results.ShouldBe(GameOutcomeEnum.Win.ToString());
        }

        [Fact]
        public async Task Handle_ShouldPublishGameResultEvent()
        {
            // Arrange
            var request = new PlayerChoiceRequest
            {
                UserId = "validUserId",
                Choice = ChoiceEnum.Rock
            };

            var computerChoice = ChoiceEnum.Scissors;
            var playerStateMock = new Mock<IChoiceState>();
            var computerStateMock = new Mock<IChoiceState>();

            playerStateMock.Setup(p => p.CalculateResult(It.IsAny<IChoiceState>()))
                .Returns(GameOutcomeEnum.Win);

            _externalApiServiceMock
                .Setup(service => service.GetRandomChoiceAsync())
                .ReturnsAsync(computerChoice);

            _choiceStateFactoryMock
                .Setup(factory => factory.GetStateForChoice(ChoiceEnum.Rock))
                .Returns(playerStateMock.Object);

            _choiceStateFactoryMock
                .Setup(factory => factory.GetStateForChoice(computerChoice))
                .Returns(computerStateMock.Object);

            // Act
            await _commandHandler.Handle(request, CancellationToken.None);

            // Assert
            _messagePublisherMock.Verify(
                publisher => publisher.PublishGameResultEvent(It.Is<GameResultEvent>(
                    e => e.UserId == request.UserId &&
                         e.PlayerChoice == request.Choice &&
                         e.ComputerChoice == computerChoice &&
                         e.Result == GameOutcomeEnum.Win)),
                Times.Once);
        }
    }
}
