﻿using FluentValidation;
using GameStatsService.Business.Requests;
using MediatR;
using Shared.Enums;

namespace GameStatsService.Business.Handlers
{
    public class AddGameResultHandler
    {
        public class RequestValidator : AbstractValidator<GameResultRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.PlayerChoice)
                    .Must(choice => Enum.IsDefined(typeof(ChoiceEnum), choice))
                    .WithMessage("Invalid choice. PlayerChoice is not in valid format.");

                RuleFor(x => x.ComputerChoice)
                    .Must(choice => Enum.IsDefined(typeof(ChoiceEnum), choice))
                    .WithMessage("Invalid choice. ComputerChoice is not in valid format.");

                RuleFor(x => x.Result)
                    .Must(choice => Enum.IsDefined(typeof(GameResultEnum), choice))
                    .WithMessage("Invalid choice. Result is not in valid format.");

                RuleFor(x => x.UserId)
                    .Must(userId => !string.IsNullOrEmpty(userId))
                    .WithMessage("UserId cannot be null or empty.");
            }
        }

        public class CommandHandler : IRequestHandler<GameResultRequest, Unit>
        {
            private readonly IRepository _repository;

            public CommandHandler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(GameResultRequest gameResultRequest, CancellationToken cancellationToken)
            {
                await _repository.AddResult(gameResultRequest, cancellationToken);

                switch (gameResultRequest.Result)
                {
                    case GameResultEnum.Win:
                        await _repository.IncrementWins();
                        break;
                    case GameResultEnum.Lose:
                        await _repository.IncrementLosses();
                        break;
                    case GameResultEnum.Tie:
                        await _repository.IncrementTies();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return Unit.Value;
            }
        }
    }
}
