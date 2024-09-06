using Shared.DTOs;

namespace GameStatsService.Business.Responses
{
    public class ScoreboardHistoryResponse
    {
        public PaginatedResult<GameResultResponse> Entries { get; set; }
    }
}
