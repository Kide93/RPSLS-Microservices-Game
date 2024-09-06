using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Requests
{
    public class UserScoreboardHistoryRequest : IRequest<ScoreboardHistoryResponse>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
