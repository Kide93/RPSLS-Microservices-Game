using ChoiceService.Models;

namespace ChoiceService.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly List<Choice> _choices =
        [
            new Choice { Id = 1, Name = "Rock" },
            new Choice { Id = 2, Name = "Paper" },
            new Choice { Id = 3, Name = "Scissors" },
            new Choice { Id = 4, Name = "Lizard" },
            new Choice { Id = 5, Name = "Spock" }
        ];

        /// <summary>
        /// Retrieves all available choices from the in-memory list.
        /// </summary>
        public List<Choice> GetAllChoices() => _choices;
    }
}
