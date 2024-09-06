using GameLogicService.Business.Responses;
using MediatR;
using Shared.Enums;

namespace GameLogicService.Business.Requests
{
    public class PlayerChoiceRequest : IRequest<GameResultResponse>
    {
        public ChoiceEnum Choice { get; set; }
        public string UserId { get; set; }
    }
}
