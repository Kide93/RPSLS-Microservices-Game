namespace ChoiceService.Services
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumberAsync();
    }
}
