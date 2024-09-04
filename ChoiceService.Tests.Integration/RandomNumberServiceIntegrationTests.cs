using ChoiceService.Services;
using ChoiceService.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ChoiceService.Tests.Integration
{
    public class RandomNumberServiceIntegrationTests
    {
        private readonly WireMockServer _mockServer;
        private readonly HttpClient _httpClient;
        private readonly RandomNumberService _randomNumberService;
        public RandomNumberServiceIntegrationTests()
        {
            _mockServer = WireMockServer.Start(9876);

            _mockServer
                .Given(Request.Create().WithPath("/random").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody("{ \"random_number\": 55 }")
                    .WithHeader("Content-Type", "application/json"));

            _httpClient = new HttpClient { BaseAddress = new System.Uri(_mockServer.Url) };
            var apiSettings = Options.Create(new ExternalApiSettings
            {
                RandomNumberApiUrl = $"{_mockServer.Url}/random"
            });

            var loggerMock = new Mock<ILogger<RandomNumberService>>();
            _randomNumberService = new RandomNumberService(_httpClient, loggerMock.Object, apiSettings);
        }

        [Fact]
        public async Task GetRandomNumberAsync_ShouldReturnNumberFromMockServer()
        {
            // Act
            var result = await _randomNumberService.GetRandomNumberAsync();

            // Assert
            Assert.Equal(55, result);
        }

        public void Dispose()
        {
            _mockServer.Stop();
            _mockServer.Dispose();
        }
    }
}
