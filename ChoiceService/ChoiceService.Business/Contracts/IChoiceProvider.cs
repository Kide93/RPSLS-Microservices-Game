using ChoiceService.Business.Models;

namespace ChoiceService.Business.Contracts
{
    public interface IChoiceProvider
    {
        List<Choice> GetAllChoices();
    }
}
