using GameLogicService.Business.Contracts;
using GameLogicService.Business.Exceptions;
using GameLogicService.Business.Models;
using GameLogicService.Business.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.DTOs;
using Shared.Enums;
using Shared.Exceptions;
using System.Text.Json;

namespace GameLogicService.Business.Implementations
{
    public class GameLogicService : IGameLogicService
    {
        private readonly IChoiceStateFactory _choiceStateFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GameLogicService> _logger;
        private readonly string _randomChoiceApiUrl;

        public GameLogicService(IChoiceStateFactory choiceStateFactory, HttpClient httpClient, ILogger<GameLogicService> logger, IOptions<ExternalApiSettings> options)
        {
            _choiceStateFactory = choiceStateFactory;
            _httpClient = httpClient;
            _logger = logger;
            _randomChoiceApiUrl = options.Value.ChoiceServiceApiUrl;
        }

        public async Task<GameResult> Play(ChoiceEnum playerChoice)
        {
            if (!Enum.IsDefined(typeof(ChoiceEnum), playerChoice))
            {
                throw new InvalidChoiceException("Invalid choice provided.");
            }

            var computerChoice = await GetRandomChoiceFromApi();

            if (!Enum.IsDefined(typeof(ChoiceEnum), playerChoice))
            {
                throw new InvalidChoiceException("Invalid choice provided.");
            }

            var playerState = _choiceStateFactory.GetStateForChoice(playerChoice);
            var computerState = _choiceStateFactory.GetStateForChoice(computerChoice);

            var result = playerState.CalculateResult(computerState);

            return new GameResult(playerChoice, computerChoice, result);
        }

        private async Task<ChoiceEnum> GetRandomChoiceFromApi()
        {
            try
            {
                var response = await _httpClient.GetAsync(_randomChoiceApiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var randomChoiceResponse = JsonSerializer.Deserialize<RandomChoiceResponseDto>(content);

                return (ChoiceEnum)randomChoiceResponse.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching random choice from the API.");
                throw new ExternalServiceException("Failed to retrieve random choice from external service.", ex);
            }
        }
    }
}
