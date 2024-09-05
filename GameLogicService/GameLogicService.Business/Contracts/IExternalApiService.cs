using Shared.Enums;

namespace GameLogicService.Business.Contracts
{
    public interface IExternalApiService
    {
        Task<ChoiceEnum> GetRandomChoiceAsync();
    }
}
