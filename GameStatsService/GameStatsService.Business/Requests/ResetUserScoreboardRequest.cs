using MediatR;

namespace GameStatsService.Business.Requests
{
    public class ResetUserScoreboardRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
