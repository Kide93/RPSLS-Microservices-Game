using FluentValidation;
using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class ResetUserScoreboardHandler
    {
        public class RequestValidator : AbstractValidator<ResetUserScoreboardRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.UserId)
                    .Must(userId => !string.IsNullOrEmpty(userId))
                    .WithMessage("UserId cannot be null or empty.");
            }
        }

        public class CommandHandler : IRequestHandler<ResetUserScoreboardRequest, Unit>
        {
            private readonly IScoreboardRepository _scoreboardRepository;

            public CommandHandler(IScoreboardRepository scoreboardRepository)
            {
                _scoreboardRepository = scoreboardRepository;
            }

            public async Task<Unit> Handle(ResetUserScoreboardRequest request, CancellationToken cancellationToken)
            {
                await _scoreboardRepository.ResetUserScoreboard(request.UserId);
                return Unit.Value;
            }
        }
    }
}
