using ChoiceService.Models;

namespace ChoiceService.Repositories
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

        /// <summary>
        /// Retrieves all available choices from the in-memory list.
        /// </summary>
        public List<Choice> GetAllChoices() => _choices;
    }
}
