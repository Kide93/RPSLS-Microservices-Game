using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Requests
{
    public class GlobalScoreboardHistoryRequest : IRequest<ScoreboardHistoryResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
