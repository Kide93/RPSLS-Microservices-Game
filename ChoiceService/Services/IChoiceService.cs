using ChoiceService.DTOs;
using Shared.DTOs;

namespace ChoiceService.Services
{
    public interface IChoiceService
    {
        /// <summary>
        /// Retrieves all available choices as DTOs.
        /// </summary>
        Task<List<ChoiceDto>> GetAllChoicesAsync();

        /// <summary>
        /// Retrieves a random choice using the external random number service.
        /// </summary>
        Task<RandomChoiceResponseDto> GetRandomChoiceAsync();
    }
}
