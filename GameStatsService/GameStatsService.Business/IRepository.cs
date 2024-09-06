using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;

namespace GameStatsService.Business
{
    public interface IRepository
    {
        Task AddResult(GameResultRequest gameResultRequest, CancellationToken cancellationToken);
        Task ResetScoreboard();
        Task<GlobalScoreboardResponse> GetGlobalScoreboard();
        Task IncrementLosses();
        Task IncrementWins();
        Task IncrementTies();
    }
}
