using ChoiceService.Business.Contracts;
using ChoiceService.Business.Models;
using ChoiceService.Business.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.DTOs;

namespace ChoiceService.Business.Handlers
{
    public class RandomChoiceHandler
    {
        public class CommandHandler : IRequestHandler<GetRandomChoiceRequest, RandomChoiceResponse>
        {
            private readonly ILogger<RandomChoiceHandler> _logger;
            private readonly IRandomNumberService _randomNumberService;

            public CommandHandler(
                ILogger<RandomChoiceHandler> logger,
                IRandomNumberService randomNumberService)
            {
                _logger = logger;
                _randomNumberService = randomNumberService;
            }

            public async Task<RandomChoiceResponse> Handle(GetRandomChoiceRequest request, CancellationToken cancellationToken)
            {
                var randomNumber = await _randomNumberService.GetRandomNumberAsync();

                var choiceId = MapRandomNumberToChoice(randomNumber);
                var randomChoice = (ChoiceEnum)choiceId;

                var choiceDto = new RandomChoiceResponse
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
}
