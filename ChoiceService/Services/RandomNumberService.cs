using ChoiceService.DTOs;
using System.Text.Json;

namespace ChoiceService.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RandomNumberService> _logger;
        private const string RandomNumberApiUrl = "https://codechallenge.boohma.com/random";

        public RandomNumberService(HttpClient httpClient, ILogger<RandomNumberService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a random number from the external API.
        /// </summary>
        public async Task<int> GetRandomNumberAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(RandomNumberApiUrl);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var randomNumberResponse = JsonSerializer.Deserialize<RandomNumberResponseDto>(content);
                return randomNumberResponse.RandomNumber;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling the random number API.");
                throw;
            }
        }
    }
}
