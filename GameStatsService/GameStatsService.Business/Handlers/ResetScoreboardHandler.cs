using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class ResetScoreboardHandler
    {
        public class CommandHandler : IRequestHandler<ResetScoreboardRequest, Unit>
        {
            private readonly IScoreboardRepository _scoreboardRepository;

            public CommandHandler(IScoreboardRepository scoreboardRepository)
            {
                _scoreboardRepository = scoreboardRepository;
            }

            public async Task<Unit> Handle(ResetScoreboardRequest request, CancellationToken cancellationToken)
            {
                await _scoreboardRepository.ResetAllScoreboard();
                return Unit.Value;
            }
        }
    }
}
