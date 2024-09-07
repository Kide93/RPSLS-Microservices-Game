using GameStatsService.Infrastructure.Mappers;
using GameStatsService.Infrastructure.Models;
using Shared.Enums;
using Shouldly;

namespace GameStatsService.Tests.Unit
{

    public class GameResultMapperTests
    {
        [Fact]
        public void ToResponse_ShouldMapGameResultToGameResultResponse()
        {
            // Arrange
            var gameResult = new GameResult
            {
                Id = 1,
                UserChoice = (int)ChoiceEnum.Rock,
                ComputerChoice = (int)ChoiceEnum.Scissors,
                Result = (int)GameOutcomeEnum.Win,
                CreatedAt = new DateTime(2024, 1, 1),
                UserId = "test-user"
            };

            // Act
            var result = gameResult.ToResponse();

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(gameResult.Id);
            result.UserChoice.ShouldBe(gameResult.UserChoice);
            result.ComputerChoice.ShouldBe(gameResult.ComputerChoice);
            result.Result.ShouldBe(GameOutcomeEnum.Win);
            result.CreatedAt.ShouldBe(gameResult.CreatedAt);
            result.UserId.ShouldBe(gameResult.UserId);
        }
    }
}
