using GameLogicService.Business.Implementations;
using GameLogicService.Business.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Shared.Enums;
using Shared.Exceptions;
using Shouldly;
using System.Net;

namespace GameLogicService.Tests.Unit
{
    public class ExternalApiServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<ILogger<ExternalApiService>> _loggerMock;
        private readonly IOptions<ExternalApiSettings> _options;
        private readonly ExternalApiService _externalApiService;

        public ExternalApiServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _loggerMock = new Mock<ILogger<ExternalApiService>>();
            _options = Options.Create(new ExternalApiSettings { ChoiceServiceApiUrl = "https://mockapi.com/random" });

            _externalApiService = new ExternalApiService(_httpClient, _loggerMock.Object, _options);
        }

        [Fact]
        public async Task GetRandomChoiceAsync_ShouldReturnChoiceEnum_WhenApiReturnsValidResponse()
        {
            // Arrange
            var responseContent = "{\"id\": 1}";
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            // Act
            var result = await _externalApiService.GetRandomChoiceAsync();

            // Assert
            result.ShouldBe(ChoiceEnum.Rock);
        }

        [Fact]
        public async Task GetRandomChoiceAsync_ShouldThrowExternalServiceException_WhenApiReturnsError()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ExternalServiceException>(() => _externalApiService.GetRandomChoiceAsync());
            exception.StatusCode.ShouldBe(HttpStatusCode.ServiceUnavailable);
        }

        [Fact]
        public async Task GetRandomChoiceAsync_ShouldThrowExternalServiceException_WhenJsonIsInvalid()
        {
            // Arrange
            var invalidJson = "{\"invalid\": \"data\"}";
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(invalidJson)
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ExternalServiceException>(() => _externalApiService.GetRandomChoiceAsync());
            exception.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetRandomChoiceAsync_ShouldLogInformationAndErrorMessages()
        {
            // Arrange
            var responseContent = "{\"id\": 1}";
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            // Act
            await _externalApiService.GetRandomChoiceAsync();

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Service URL: https://mockapi.com/random")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Never);
        }
    }
}
