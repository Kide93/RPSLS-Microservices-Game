using FluentValidation;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    internal class UserScoreboardHistoryHandler
    {
        public class RequestValidator : AbstractValidator<UserScoreboardHistoryRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.UserId)
                    .Must(userId => !string.IsNullOrEmpty(userId))
                    .WithMessage("UserId cannot be null or empty.");
            }
        }

        public class CommandHandler : IRequestHandler<UserScoreboardHistoryRequest, ScoreboardHistoryResponse>
        {
            private readonly IRepository _repository;

            public CommandHandler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<ScoreboardHistoryResponse> Handle(UserScoreboardHistoryRequest request, CancellationToken cancellationToken)
            {
                var result = await _repository.GetGameResultsAsync(request.UserId, request.PageNumber, request.PageSize);

                return new ScoreboardHistoryResponse
                {
                    Entries = result
                };
            }
        }
    }
}
