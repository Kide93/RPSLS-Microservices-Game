using ChoiceService.Business.Contracts;
using ChoiceService.Business.Models;

namespace ChoiceService.Business.Implementations
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly List<Choice> _choices;

        public ChoiceRepository()
        {
            _choices = Enum.GetValues(typeof(ChoiceEnum))
                .Cast<ChoiceEnum>()
                .Select(e => new Choice
                {
                    Id = (int)e,
                    Name = e.ToString()
                })
                .ToList();
        }

        public List<Choice> GetAllChoices() => _choices;
    }
}
