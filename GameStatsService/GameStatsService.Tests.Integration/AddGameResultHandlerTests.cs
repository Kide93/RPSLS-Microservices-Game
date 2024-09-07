using GameStatsService.Business.Handlers;
using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Enums;

namespace GameStatsService.Tests.Integration
{
    public class AddGameResultHandlerTests
    {
        private readonly IMediator _mediator;
        private readonly Mock<IScoreboardRepository> _scoreboardRepositoryMock;
        private readonly Mock<IGameResultsRepository> _gameResultsRepositoryMock;

        public AddGameResultHandlerTests()
        {
            _scoreboardRepositoryMock = new Mock<IScoreboardRepository>();
            _gameResultsRepositoryMock = new Mock<IGameResultsRepository>();

            var services = new ServiceCollection();
            services.AddMediatR(typeof(AddGameResultHandler.CommandHandler).Assembly);
            services.AddScoped(_ => _scoreboardRepositoryMock.Object);
            services.AddScoped(_ => _gameResultsRepositoryMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldCallAddResultAndIncrementWins_WhenResultIsWin()
        {
            // Arrange
            var request = new GameResultRequest
            {
                UserId = "validUserId",
                PlayerChoice = ChoiceEnum.Rock,
                ComputerChoice = ChoiceEnum.Scissors,
                Result = GameOutcomeEnum.Win
            };

            // Act
            await _mediator.Send(request, CancellationToken.None);

            // Assert
            _gameResultsRepositoryMock.Verify(repo => repo.AddResult(request, CancellationToken.None), Times.Once);
            _scoreboardRepositoryMock.Verify(repo => repo.IncrementWins(), Times.Once);
            _scoreboardRepositoryMock.Verify(repo => repo.IncrementWins(request.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallAddResultAndIncrementLosses_WhenResultIsLose()
        {
            // Arrange
            var request = new GameResultRequest
            {
                UserId = "validUserId",
                PlayerChoice = ChoiceEnum.Rock,
                ComputerChoice = ChoiceEnum.Paper,
                Result = GameOutcomeEnum.Lose
            };

            // Act
            await _mediator.Send(request, CancellationToken.None);

            // Assert
            _gameResultsRepositoryMock.Verify(repo => repo.AddResult(request, CancellationToken.None), Times.Once);
            _scoreboardRepositoryMock.Verify(repo => repo.IncrementLosses(), Times.Once);
            _scoreboardRepositoryMock.Verify(repo => repo.IncrementLosses(request.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallAddResultAndIncrementTies_WhenResultIsTie()
        {
            // Arrange
            var request = new GameResultRequest
            {
                UserId = "validUserId",
                PlayerChoice = ChoiceEnum.Rock,
                ComputerChoice = ChoiceEnum.Rock,
                Result = GameOutcomeEnum.Tie
            };

            // Act
            await _mediator.Send(request, CancellationToken.None);

            // Assert
            _gameResultsRepositoryMock.Verify(repo => repo.AddResult(request, CancellationToken.None), Times.Once);
            _scoreboardRepositoryMock.Verify(repo => repo.IncrementTies(), Times.Once);
            _scoreboardRepositoryMock.Verify(repo => repo.IncrementTies(request.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentOutOfRangeException_WhenResultIsInvalid()
        {
            // Arrange
            var request = new GameResultRequest
            {
                UserId = "validUserId",
                PlayerChoice = ChoiceEnum.Rock,
                ComputerChoice = ChoiceEnum.Scissors,
                Result = (GameOutcomeEnum)999
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _mediator.Send(request, CancellationToken.None));
        }
    }
}
