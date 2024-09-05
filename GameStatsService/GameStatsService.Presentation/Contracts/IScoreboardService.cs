using GameStatsService.Presentation.Models;
using Shared.Events;

namespace GameStatsService.Presentation.Contracts
{
    public interface IScoreboardService
    {
        Task UpdateScoreboard(GameResultEvent gameResultEvent);
        Task<Scoreboard> GetGlobalScoreboard();
        Task<Scoreboard> GetUserScoreboard(string userId);
    }
}
