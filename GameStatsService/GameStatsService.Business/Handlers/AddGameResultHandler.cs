using FluentValidation;
using GameStatsService.Business.Repositories;
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
                    .Must(choice => Enum.IsDefined(typeof(GameOutcomeEnum), choice))
                    .WithMessage("Invalid choice. Result is not in valid format.");

                RuleFor(x => x.UserId)
                    .Must(userId => !string.IsNullOrEmpty(userId))
                    .WithMessage("UserId cannot be null or empty.");
            }
        }

        public class CommandHandler : IRequestHandler<GameResultRequest, Unit>
        {
            private readonly IScoreboardRepository _scoreboardRepository;
            private readonly IGameResultsRepository _gameResultsRepository;

            public CommandHandler(IScoreboardRepository scoreboardRepository, IGameResultsRepository gameResultsRepository)
            {
                _scoreboardRepository = scoreboardRepository;
                _gameResultsRepository = gameResultsRepository;
            }

            public async Task<Unit> Handle(GameResultRequest gameResultRequest, CancellationToken cancellationToken)
            {
                await _gameResultsRepository.AddResult(gameResultRequest, cancellationToken);

                switch (gameResultRequest.Result)
                {
                    case GameOutcomeEnum.Win:
                        await _scoreboardRepository.IncrementWins();
                        await _scoreboardRepository.IncrementWins(gameResultRequest.UserId);
                        break;
                    case GameOutcomeEnum.Lose:
                        await _scoreboardRepository.IncrementLosses();
                        await _scoreboardRepository.IncrementLosses(gameResultRequest.UserId);
                        break;
                    case GameOutcomeEnum.Tie:
                        await _scoreboardRepository.IncrementTies();
                        await _scoreboardRepository.IncrementTies(gameResultRequest.UserId);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return Unit.Value;
            }
        }
    }
}
