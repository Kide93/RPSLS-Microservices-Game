using ChoiceService.Business.Contracts;
using ChoiceService.Business.Requests;
using ChoiceService.Business.Responses;
using MediatR;

namespace ChoiceService.Business.Handlers
{
    public class ChoicesHandler
    {
        public class CommandHandler : IRequestHandler<GetChoicesRequest, ChoicesResponse>
        {
            private readonly IChoiceProvider _choiceProvider;

            public CommandHandler(IChoiceProvider choiceProvider)
            {
                _choiceProvider = choiceProvider;
            }

            public async Task<ChoicesResponse> Handle(GetChoicesRequest request, CancellationToken cancellationToken)
            {
                var choices = _choiceProvider.GetAllChoices();

                var choiceResponses = choices.Select(c => new ChoiceResponse
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                return new ChoicesResponse
                {
                    Choices = choiceResponses
                };
            }
        }
    }
}
