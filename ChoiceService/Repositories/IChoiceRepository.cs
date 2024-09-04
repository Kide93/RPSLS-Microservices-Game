using ChoiceService.Models;

namespace ChoiceService.Repositories
{
    public interface IChoiceRepository
    {
        List<Choice> GetAllChoices();
    }
}
