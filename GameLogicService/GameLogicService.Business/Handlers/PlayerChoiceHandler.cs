using FluentValidation;
using GameLogicService.Business.Contracts;
using GameLogicService.Business.Models;
using MediatR;
using Shared.Enums;

namespace GameLogicService.Business.Handlers
{
    public class PlayerChoiceHandler
    {
        public class RequestValidator : AbstractValidator<PlayerChoiceRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Choice)
                    .Must(choice => Enum.IsDefined(typeof(ChoiceEnum), choice))
                    .WithMessage("Invalid choice. Please select a valid option.");
            }
        }

        public class CommandHandler : IRequestHandler<PlayerChoiceRequest, GameResultResponse>
        {
            private readonly IChoiceStateFactory _choiceStateFactory;
            private readonly IExternalApiService _externalApiService;

            public CommandHandler(
                IChoiceStateFactory choiceStateFactory,
                IExternalApiService externalApiService)
            {
                _choiceStateFactory = choiceStateFactory;
                _externalApiService = externalApiService;
            }

            public async Task<GameResultResponse> Handle(PlayerChoiceRequest request, CancellationToken cancellationToken)
            {
                var computerChoice = await _externalApiService.GetRandomChoiceAsync();

                var playerState = _choiceStateFactory.GetStateForChoice(request.Choice);
                var computerState = _choiceStateFactory.GetStateForChoice(computerChoice);

                var result = playerState.CalculateResult(computerState);

                return new GameResultResponse(request.Choice, computerChoice, result);
            }
        }
    }
}
