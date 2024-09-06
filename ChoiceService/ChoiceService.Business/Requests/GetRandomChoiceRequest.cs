using MediatR;
using Shared.DTOs;

namespace ChoiceService.Business.Requests
{
    public class GetRandomChoiceRequest : IRequest<RandomChoiceResponse>
    {
    }
}
