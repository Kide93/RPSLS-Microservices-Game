using ChoiceService.Business.DTOs;
using Shared.DTOs;

namespace ChoiceService.Business.Contracts
{
    public interface IChoiceService
    {
        Task<List<ChoiceDto>> GetAllChoicesAsync();

        Task<RandomChoiceResponseDto> GetRandomChoiceAsync();
    }
}
