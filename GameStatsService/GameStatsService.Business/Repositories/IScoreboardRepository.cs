using GameStatsService.Business.Responses;

namespace GameStatsService.Business.Repositories
{
    public interface IScoreboardRepository
    {
        Task ResetAllScoreboard();
        Task ResetUserScoreboard(string userId);
        Task<ScoreboardResponse> GetGlobalScoreboard();
        Task<ScoreboardResponse> GetUserScoreboard(string userId);
        Task IncrementLosses();
        Task IncrementWins();
        Task IncrementTies();
        Task IncrementLosses(string userId);
        Task IncrementWins(string userId);
        Task IncrementTies(string userId);
    }
}
