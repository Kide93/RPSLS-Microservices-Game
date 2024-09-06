using FluentValidation;
using GameLogicService.Business.Contracts;
using GameLogicService.Business.Requests;
using GameLogicService.Business.Responses;
using MediatR;
using Shared.Enums;
using Shared.Events;

namespace GameLogicService.Business.Handlers
{
    public class PlayerChoiceHandler
    {
        public class RequestValidator : AbstractValidator<PlayerChoiceRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.UserId)
                    .Must(userId => !string.IsNullOrEmpty(userId))
                    .WithMessage("UserId cannot be null or empty.");

                RuleFor(x => x.Choice)
                    .Must(choice => Enum.IsDefined(typeof(ChoiceEnum), choice))
                    .WithMessage("Invalid choice. Please select a valid option.");
            }
        }

        public class CommandHandler : IRequestHandler<PlayerChoiceRequest, GameResultResponse>
        {
            private readonly IChoiceStateFactory _choiceStateFactory;
            private readonly IExternalApiService _externalApiService;
            private readonly IMessagePublisher _messagePublisher;

            public CommandHandler(
                IChoiceStateFactory choiceStateFactory,
                IExternalApiService externalApiService,
                IMessagePublisher messagePublisher)
            {
                _choiceStateFactory = choiceStateFactory;
                _externalApiService = externalApiService;
                _messagePublisher = messagePublisher;
            }

            public async Task<GameResultResponse> Handle(PlayerChoiceRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var computerChoice = await _externalApiService.GetRandomChoiceAsync();
                    var playerState = _choiceStateFactory.GetStateForChoice(request.Choice);
                    var computerState = _choiceStateFactory.GetStateForChoice(computerChoice);

                    var result = playerState.CalculateResult(computerState);

                    var gameResultEvent = new GameResultEvent
                    {
                        UserId = request.UserId,
                        PlayerChoice = request.Choice,
                        ComputerChoice = computerChoice,
                        Result = result,
                        Timestamp = DateTime.UtcNow
                    };

                    await _messagePublisher.PublishGameResultEvent(gameResultEvent);

                    return new GameResultResponse(request.Choice, computerChoice, result);
                }
                catch (Exception e)
                {
                    // TODO: Handle
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
