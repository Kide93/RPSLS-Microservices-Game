using FluentValidation;
using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class GetUserScoreboardHandler
    {
        public class RequestValidator : AbstractValidator<UserScoreboardRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.UserId)
                    .Must(userId => !string.IsNullOrEmpty(userId))
                    .WithMessage("UserId cannot be null or empty.");
            }
        }

        public class CommandHandler : IRequestHandler<UserScoreboardRequest, ScoreboardResponse>
        {
            private readonly IScoreboardRepository _scoreboardRepository;

            public CommandHandler(IScoreboardRepository scoreboardRepository)
            {
                _scoreboardRepository = scoreboardRepository;
            }

            public async Task<ScoreboardResponse> Handle(UserScoreboardRequest request, CancellationToken cancellationToken)
            {
                return await _scoreboardRepository.GetUserScoreboard(request.UserId);
            }
        }
    }
}
