namespace ChoiceService.Business.Contracts
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumberAsync();
    }
}
