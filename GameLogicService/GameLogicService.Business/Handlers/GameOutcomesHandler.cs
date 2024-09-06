using GameLogicService.Business.Models;
using GameLogicService.Business.Requests;
using GameLogicService.Business.Responses;
using MediatR;
using Shared.Enums;

namespace GameLogicService.Business.Handlers
{
    public class GameOutcomesHandler
    {
        public class CommandHandler : IRequestHandler<GameOutcomeRequest, GameOutcomeResponse>
        {
            public async Task<GameOutcomeResponse> Handle(GameOutcomeRequest request, CancellationToken cancellationToken)
            {
                var outcomes = Enum.GetValues(typeof(GameOutcomeEnum))
                    .Cast<GameOutcomeEnum>()
                    .Select(gr => new GameOutcome
                    {
                        Id = (int)gr,
                        Name = gr.ToString()
                    })
                    .ToList();

                return new GameOutcomeResponse
                {
                    GameOutcomes = outcomes
                };
            }
        }
    }
}
