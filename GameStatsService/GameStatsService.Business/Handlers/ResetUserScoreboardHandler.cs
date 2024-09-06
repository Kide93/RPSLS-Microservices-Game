using FluentValidation;
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
            private readonly IRepository _repository;

            public CommandHandler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(ResetUserScoreboardRequest request, CancellationToken cancellationToken)
            {
                await _repository.ResetUserScoreboard(request.UserId);
                return Unit.Value;
            }
        }
    }
}
