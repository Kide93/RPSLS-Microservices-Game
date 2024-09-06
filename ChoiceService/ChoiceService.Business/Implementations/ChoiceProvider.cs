using ChoiceService.Business.Contracts;
using ChoiceService.Business.Models;

namespace ChoiceService.Business.Implementations
{
    public class ChoiceProvider : IChoiceProvider
    {
        private readonly List<Choice> _choices;

        public ChoiceProvider()
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
