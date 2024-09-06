using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class GlobalScoreboardHistoryHandler
    {
        public class CommandHandler : IRequestHandler<GlobalScoreboardHistoryRequest, ScoreboardHistoryResponse>
        {
            private readonly IRepository _repository;

            public CommandHandler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<ScoreboardHistoryResponse> Handle(GlobalScoreboardHistoryRequest request, CancellationToken cancellationToken)
            {
                var result = await _repository.GetGameResultsAsync(request.PageNumber, request.PageSize);

                return new ScoreboardHistoryResponse
                {
                    Entries = result
                };
            }
        }

    }
}
