using ChoiceService.Business.Contracts;
using ChoiceService.Business.DTOs;
using ChoiceService.Business.Models;
using Microsoft.Extensions.Logging;
using Shared.DTOs;

namespace ChoiceService.Business.Implementations
{
    public class ChoiceService : IChoiceService
    {
        private readonly ILogger<ChoiceService> _logger;
        private readonly IChoiceRepository _choiceRepository;
        private readonly IRandomNumberService _randomNumberService;

        public ChoiceService(IChoiceRepository choiceRepository, IRandomNumberService randomNumberService, ILogger<ChoiceService> logger)
        {
            _choiceRepository = choiceRepository;
            _randomNumberService = randomNumberService;
            _logger = logger;
        }

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

        public async Task<RandomChoiceResponseDto> GetRandomChoiceAsync()
        {
            var randomNumber = await _randomNumberService.GetRandomNumberAsync();

            var choiceId = MapRandomNumberToChoice(randomNumber);
            var randomChoice = (ChoiceEnum)choiceId;

            var choiceDto = new RandomChoiceResponseDto
            {
                Id = (int)randomChoice,
                Name = randomChoice.ToString()
            };

            return choiceDto;
        }

        private int MapRandomNumberToChoice(int randomNumber)
        {
            if (randomNumber < 1 || randomNumber > 100)
            {
                _logger.LogWarning($"Random number {randomNumber} is out of the expected range (1-100). Returning default choice (Spock).");
                return (int)ChoiceEnum.Spock;
            }

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
