using ChoiceService.DTOs;
using ChoiceService.Models;
using ChoiceService.Repositories;

namespace ChoiceService.Services
{
    public class ChoiceService : IChoiceService
    {
        private readonly IChoiceRepository _choiceRepository;
        private readonly IRandomNumberService _randomNumberService;

        public ChoiceService(IChoiceRepository choiceRepository, IRandomNumberService randomNumberService)
        {
            _choiceRepository = choiceRepository;
            _randomNumberService = randomNumberService;
        }

        /// <summary>
        /// Retrieves all available choices and maps them to DTOs.
        /// </summary>
        public async Task<List<ChoiceDto>> GetAllChoicesAsync()
        {
            var choices = _choiceRepository.GetAllChoices();

            var choicesDto = choices.Select(c => new ChoiceDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return await Task.FromResult(choicesDto);
        }

        /// <summary>
        /// Retrieves a random choice using the external random number service.
        /// </summary>
        public async Task<ChoiceDto> GetRandomChoiceAsync()
        {
            var randomNumber = await _randomNumberService.GetRandomNumberAsync();

            var choiceId = MapRandomNumberToChoice(randomNumber);

            var randomChoice = (ChoiceEnum)choiceId;

            var choiceDto = new ChoiceDto
            {
                Id = (int)randomChoice,
                Name = randomChoice.ToString()
            };

            return choiceDto;
        }

        /// <summary>
        /// Maps the random number to one of the choices.
        /// </summary>
        private int MapRandomNumberToChoice(int randomNumber)
        {
            return randomNumber switch
            {
                >= 1 and <= 20 => (int)ChoiceEnum.Rock,
                >= 21 and <= 40 => (int)ChoiceEnum.Paper,
                >= 41 and <= 60 => (int)ChoiceEnum.Scissors,
                >= 61 and <= 80 => (int)ChoiceEnum.Lizard,
                _ => (int)ChoiceEnum.Spock
            };
        }
    }
}
