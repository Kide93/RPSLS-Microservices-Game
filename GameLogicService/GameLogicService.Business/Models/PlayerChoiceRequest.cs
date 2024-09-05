using MediatR;
using Shared.Enums;

namespace GameLogicService.Business.Models
{
    public class PlayerChoiceRequest : IRequest<GameResultResponse>
    {
        public ChoiceEnum Choice { get; set; }
    }
}
