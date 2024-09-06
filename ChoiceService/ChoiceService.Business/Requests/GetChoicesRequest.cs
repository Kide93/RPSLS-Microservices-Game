using ChoiceService.Business.Responses;
using MediatR;

namespace ChoiceService.Business.Requests
{
    public class GetChoicesRequest : IRequest<ChoicesResponse>
    {
    }
}
