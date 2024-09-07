using GameStatsService.Business.Handlers;
using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using Moq;
using Shared.Enums;

namespace GameStatsService.Tests.Unit
{
    public class CommandHandlerTests
    {
        private readonly Mock<IScoreboardRepository> _scoreboardRepositoryMock;
        private readonly Mock<IGameResultsRepository> _gameResultsRepositoryMock;
        private readonly AddGameResultHandler.CommandHandler _commandHandler;

        public CommandHandlerTests()
        {
            _scoreboardRepositoryMock = new Mock<IScoreboardRepository>();
            _gameResultsRepositoryMock = new Mock<IGameResultsRepository>();

            _commandHandler = new AddGameResultHandler.CommandHandler(
                _scoreboardRepositoryMock.Object,
                _gameResultsRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldIncrementWins_WhenResultIsWin()
        {
            // Arrange
            var gameResultRequest = new GameResultRequest
            {
                UserId = "validUserId",
                Result = GameOutcomeEnum.Win
            };

            // Act
            await _commandHandler.Handle(gameResultRequest, CancellationToken.None);

            // Assert
            _gameResultsRepositoryMock.Verify(r => r.AddResult(gameResultRequest, CancellationToken.None), Times.Once);
            _scoreboardRepositoryMock.Verify(r => r.IncrementWins(), Times.Once);
            _scoreboardRepositoryMock.Verify(r => r.IncrementWins(gameResultRequest.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldIncrementLosses_WhenResultIsLose()
        {
            // Arrange
            var gameResultRequest = new GameResultRequest
            {
                UserId = "validUserId",
                Result = GameOutcomeEnum.Lose
            };

            // Act
            await _commandHandler.Handle(gameResultRequest, CancellationToken.None);

            // Assert
            _gameResultsRepositoryMock.Verify(r => r.AddResult(gameResultRequest, CancellationToken.None), Times.Once);
            _scoreboardRepositoryMock.Verify(r => r.IncrementLosses(), Times.Once);
            _scoreboardRepositoryMock.Verify(r => r.IncrementLosses(gameResultRequest.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldIncrementTies_WhenResultIsTie()
        {
            // Arrange
            var gameResultRequest = new GameResultRequest
            {
                UserId = "validUserId",
                Result = GameOutcomeEnum.Tie
            };

            // Act
            await _commandHandler.Handle(gameResultRequest, CancellationToken.None);

            // Assert
            _gameResultsRepositoryMock.Verify(r => r.AddResult(gameResultRequest, CancellationToken.None), Times.Once);
            _scoreboardRepositoryMock.Verify(r => r.IncrementTies(), Times.Once);
            _scoreboardRepositoryMock.Verify(r => r.IncrementTies(gameResultRequest.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentOutOfRangeException_WhenResultIsInvalid()
        {
            // Arrange
            var gameResultRequest = new GameResultRequest
            {
                UserId = "validUserId",
                Result = (GameOutcomeEnum)999
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _commandHandler.Handle(gameResultRequest, CancellationToken.None));
        }
    }
}
