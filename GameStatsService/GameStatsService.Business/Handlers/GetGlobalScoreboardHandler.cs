using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using MediatR;

namespace GameStatsService.Business.Handlers
{
    public class GetGlobalScoreboardHandler
    {
        public class CommandHandler : IRequestHandler<GlobalScoreboardRequest, GlobalScoreboardResponse>
        {
            private readonly IRepository _repository;

            public CommandHandler(IRepository repository)
            {
                _repository = repository;
            }

            public async Task<GlobalScoreboardResponse> Handle(GlobalScoreboardRequest request, CancellationToken cancellationToken)
            {
                return await _repository.GetGlobalScoreboard();
            }
        }
    }
}
