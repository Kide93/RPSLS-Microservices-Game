using ChoiceService.Exceptions;
using ChoiceService.Services;
using ChoiceService.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Shouldly;
using System.Net;

namespace ChoiceService.Tests.Unit
{
    public class RandomNumberServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly Mock<ILogger<RandomNumberService>> _loggerMock;
        private readonly RandomNumberService _randomNumberService;
        private readonly HttpClient _httpClient;

        public RandomNumberServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _loggerMock = new Mock<ILogger<RandomNumberService>>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            var apiSettings = Options.Create(new ExternalApiSettings
            {
                RandomNumberApiUrl = "https://mockapi.com/random"
            });
            _randomNumberService = new RandomNumberService(_httpClient, _loggerMock.Object, apiSettings);
        }

        [Fact]
        public async Task GetRandomNumberAsync_ShouldReturnRandomNumber_WhenApiCallIsSuccessful()
        {
            // Arrange
            var randomNumber = 42;
            var responseContent = $"{{\"random_number\": {randomNumber}}}";
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseContent)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _randomNumberService.GetRandomNumberAsync();

            // Assert
            result.ShouldBe(randomNumber);
        }

        [Fact]
        public async Task GetRandomNumberAsync_ShouldReturnFallbackNumber_WhenApiCallFails()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _randomNumberService.GetRandomNumberAsync();

            // Assert
            result.ShouldBeInRange(1, 100);
        }

        [Fact]
        public async Task GetRandomNumberAsync_ShouldThrowExternalServiceException_OnException()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new Exception("Critical failure"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ExternalServiceException>(() => _randomNumberService.GetRandomNumberAsync());
        }
    }
}
