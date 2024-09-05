using ChoiceService.DTOs;
using ChoiceService.Settings;
using Microsoft.Extensions.Options;
using Shared.Exceptions;
using System.Text.Json;

namespace ChoiceService.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RandomNumberService> _logger;
        private readonly string _randomNumberApiUrl;

        public RandomNumberService(HttpClient httpClient, ILogger<RandomNumberService> logger, IOptions<ExternalApiSettings> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _randomNumberApiUrl = options.Value.RandomNumberApiUrl;
        }

        /// <summary>
        /// Retrieves a random number from the external API.
        /// </summary>
        public async Task<int> GetRandomNumberAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_randomNumberApiUrl);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var randomNumberResponse = JsonSerializer.Deserialize<RandomNumberResponseDto>(content);
                return randomNumberResponse.RandomNumber;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Error calling the random number API.");
                return GetFallbackRandomNumber();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing the response from the random number API.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical failure.");
                throw new ExternalServiceException("Failed to retrieve random number after multiple retries.", ex);
            }
        }

        private int GetFallbackRandomNumber()
        {
            _logger.LogWarning("Using fallback random number due to API failure.");
            var random = new Random();
            return random.Next(1, 101);
        }
    }
}
