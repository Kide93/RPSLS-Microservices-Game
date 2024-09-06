using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Requests
{
    public class GlobalScoreboardRequest : IRequest<ScoreboardResponse>
    {
    }
}
