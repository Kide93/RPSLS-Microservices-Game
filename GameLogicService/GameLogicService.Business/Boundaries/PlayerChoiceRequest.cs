using GameLogicService.Business.Models;
using MediatR;
using Shared.Enums;

namespace GameLogicService.Business.Boundaries
{
    public class PlayerChoiceRequest : IRequest<GameResultResponse>
    {
        public ChoiceEnum Choice { get; set; }
    }
}
