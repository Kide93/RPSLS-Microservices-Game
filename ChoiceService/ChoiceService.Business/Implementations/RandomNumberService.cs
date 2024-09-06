using ChoiceService.Business.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Exceptions;
using System.Net;
using System.Text.Json;
using ChoiceService.Business.Responses;
using ChoiceService.Business.Settings;

namespace ChoiceService.Business.Implementations
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

                var randomNumberResponse = JsonSerializer.Deserialize<RandomNumberResponse>(content);
                return randomNumberResponse.RandomNumber;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error connecting to external service.");
                return GetFallbackRandomNumber();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error parsing response from external service.");
                throw new ExternalServiceException("Invalid response from external service.", ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical failure.");
                throw new ExternalServiceException("Failed to retrieve random number after multiple retries.", ex, HttpStatusCode.InternalServerError);
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
