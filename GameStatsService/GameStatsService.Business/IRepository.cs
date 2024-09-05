using GameStatsService.Business.Requests;

namespace GameStatsService.Business
{
    public interface IRepository
    {
        Task AddResult(GameResultRequest gameResultRequest, CancellationToken cancellationToken);
        Task ResetScoreboard();
        Task IncrementLosses();
        Task IncrementWins();
        Task IncrementTies();
    }
}
