using ChoiceService.Business.Models;

namespace ChoiceService.Business.Contracts
{
    public interface IChoiceRepository
    {
        List<Choice> GetAllChoices();
    }
}
