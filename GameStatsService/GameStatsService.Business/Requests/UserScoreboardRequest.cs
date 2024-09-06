using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Requests
{
    public class UserScoreboardRequest : IRequest<ScoreboardResponse>
    {
        public string UserId { get; set; }
    }
}
