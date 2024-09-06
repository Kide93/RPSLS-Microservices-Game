using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using Shared.DTOs;

namespace GameStatsService.Business.Repositories
{
    public interface IGameResultsRepository
    {
        Task AddResult(GameResultRequest gameResultRequest, CancellationToken cancellationToken);
        Task<PaginatedResult<GameResultResponse>> GetGameResultsAsync(int pageNumber, int pageSize);
        Task<PaginatedResult<GameResultResponse>> GetGameResultsAsync(string userId, int pageNumber, int pageSize);
    }
}
