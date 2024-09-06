using GameStatsService.Business.Requests;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class ResetScoreboardHandler
    {
        public class CommandHandler : IRequestHandler<ResetScoreboardRequest, Unit>
        {
            private readonly IRepository _repository;

            public CommandHandler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(ResetScoreboardRequest request, CancellationToken cancellationToken)
            {
                await _repository.ResetAllScoreboard();
                return Unit.Value;
            }
        }
    }
}
