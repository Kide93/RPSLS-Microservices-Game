using GameLogicService.Business.Contracts;
using GameLogicService.Business.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.DTOs;
using Shared.Enums;
using Shared.Exceptions;
using System.Net;
using System.Text.Json;

namespace GameLogicService.Business.Implementations
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;
        private readonly string _randomChoiceApiUrl;

        public ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger, IOptions<ExternalApiSettings> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _randomChoiceApiUrl = options.Value.ChoiceServiceApiUrl;
        }

        public async Task<ChoiceEnum> GetRandomChoiceAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_randomChoiceApiUrl);
                _logger.LogInformation($"Service URL: {_randomChoiceApiUrl}");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var randomChoiceResponse = JsonSerializer.Deserialize<RandomChoiceResponseDto>(content);

                return (ChoiceEnum)randomChoiceResponse.Id;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error connecting to external service.");
                throw new ExternalServiceException("External service unavailable.", ex, HttpStatusCode.ServiceUnavailable);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error parsing response from external service.");
                throw new ExternalServiceException("Invalid response from external service.", ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching random choice.");
                throw new ExternalServiceException("Failed to retrieve random choice from external service.", ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
