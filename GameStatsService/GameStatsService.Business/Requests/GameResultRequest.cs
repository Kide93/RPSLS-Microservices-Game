using MediatR;
using Shared.Enums;

namespace GameStatsService.Business.Requests
{
    public class GameResultRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
        public ChoiceEnum PlayerChoice { get; set; }
        public ChoiceEnum ComputerChoice { get; set; }
        public GameOutcomeEnum Result { get; set; }
    }
}
