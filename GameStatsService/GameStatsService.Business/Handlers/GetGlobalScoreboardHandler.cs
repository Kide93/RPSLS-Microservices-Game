using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class GetGlobalScoreboardHandler
    {
        public class CommandHandler : IRequestHandler<GlobalScoreboardRequest, ScoreboardResponse>
        {
            private readonly IScoreboardRepository _scoreboardRepository;

            public CommandHandler(IScoreboardRepository scoreboardRepository)
            {
                _scoreboardRepository = scoreboardRepository;
            }

            public async Task<ScoreboardResponse> Handle(GlobalScoreboardRequest request, CancellationToken cancellationToken)
            {
                return await _scoreboardRepository.GetGlobalScoreboard();
            }
        }
    }
}
