using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class GlobalScoreboardHistoryHandler
    {
        public class CommandHandler : IRequestHandler<GlobalScoreboardHistoryRequest, ScoreboardHistoryResponse>
        {
            private readonly IGameResultsRepository _gameResultsRepository;

            public CommandHandler(IGameResultsRepository scoreboardRepository)
            {
                _gameResultsRepository = scoreboardRepository;
            }

            public async Task<ScoreboardHistoryResponse> Handle(GlobalScoreboardHistoryRequest request, CancellationToken cancellationToken)
            {
                var result = await _gameResultsRepository.GetGameResultsAsync(request.PageNumber, request.PageSize);

                return new ScoreboardHistoryResponse
                {
                    Entries = result
                };
            }
        }
    }
}
