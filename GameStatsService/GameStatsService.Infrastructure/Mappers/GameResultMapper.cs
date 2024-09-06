using GameStatsService.Business.Responses;
using GameStatsService.Infrastructure.Models;
using Shared.Enums;

namespace GameStatsService.Infrastructure.Mappers
{
    public static class GameResultMapper
    {
        public static GameResultResponse ToResponse(this GameResult gameResult)
            => new GameResultResponse
            {
                Id = gameResult.Id,
                UserChoice = gameResult.UserChoice,
                ComputerChoice = gameResult.ComputerChoice,
                Result = (GameOutcomeEnum)gameResult.Result,
                CreatedAt = gameResult.CreatedAt,
                UserId = gameResult.UserId
            };
    }
}
