using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using Shared.DTOs;

namespace GameStatsService.Business
{
    public interface IRepository
    {
        Task AddResult(GameResultRequest gameResultRequest, CancellationToken cancellationToken);
        Task ResetAllScoreboard();
        Task ResetUserScoreboard(string userId);
        Task<ScoreboardResponse> GetGlobalScoreboard();
        Task<ScoreboardResponse> GetUserScoreboard(string userId);
        Task<PaginatedResult<GameResultResponse>> GetGameResultsAsync(int pageNumber, int pageSize);
        Task<PaginatedResult<GameResultResponse>> GetGameResultsAsync(string userId, int pageNumber, int pageSize);
        Task IncrementLosses();
        Task IncrementWins();
        Task IncrementTies();
        Task IncrementLosses(string userId);
        Task IncrementWins(string userId);
        Task IncrementTies(string userId);
    }
}
