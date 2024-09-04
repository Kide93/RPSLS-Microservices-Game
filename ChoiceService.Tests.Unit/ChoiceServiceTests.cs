using ChoiceService.Models;
using ChoiceService.Repositories;
using ChoiceService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace ChoiceService.Tests.Unit
{
    public class ChoiceServiceTests
    {
        private readonly Mock<IChoiceRepository> _choiceRepositoryMock;
        private readonly Mock<IRandomNumberService> _randomNumberServiceMock;
        private readonly Services.ChoiceService _choiceService;
        private readonly Mock<ILogger<Services.ChoiceService>> _loggerMock;

        public ChoiceServiceTests()
        {
            _choiceRepositoryMock = new Mock<IChoiceRepository>();
            _randomNumberServiceMock = new Mock<IRandomNumberService>();
            _loggerMock = new Mock<ILogger<Services.ChoiceService>>();
            _choiceService = new Services.ChoiceService(_choiceRepositoryMock.Object, _randomNumberServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllChoicesAsync_ShouldReturnMappedChoiceDTOs()
        {
            // Arrange
            var choices = new List<Choice>
            {
                new Choice { Id = 1, Name = "Rock" },
                new Choice { Id = 2, Name = "Paper" },
                new Choice { Id = 3, Name = "Scissors" }
            };

            _choiceRepositoryMock.Setup(repo => repo.GetAllChoices()).Returns(choices);

            // Act
            var result = await _choiceService.GetAllChoicesAsync();

            // Assert
            result.Count.ShouldBe(choices.Count);
            choices[0].Id.ShouldBe(choices[0].Id);
            result[0].Name.ShouldBe(choices[0].Name);
            result[1].Id.ShouldBe(choices[1].Id);
            result[1].Name.ShouldBe(choices[1].Name);
        }

        [Fact]
        public async Task GetRandomChoiceAsync_ShouldReturnCorrectChoiceDto_ForRandomNumber()
        {
            // Arrange
            var randomNumber = 25;
            _randomNumberServiceMock.Setup(service => service.GetRandomNumberAsync()).ReturnsAsync(randomNumber);

            // Act
            var result = await _choiceService.GetRandomChoiceAsync();

            // Assert
            result.Id.ShouldBe((int)ChoiceEnum.Paper);
            result.Name.ShouldBe(ChoiceEnum.Paper.ToString());
        }

        [Theory]
        [InlineData(-1, ChoiceEnum.Spock)]
        [InlineData(101, ChoiceEnum.Spock)]
        [InlineData(10, ChoiceEnum.Rock)]
        [InlineData(30, ChoiceEnum.Paper)]
        [InlineData(50, ChoiceEnum.Scissors)]
        [InlineData(70, ChoiceEnum.Lizard)]
        [InlineData(90, ChoiceEnum.Spock)]
        public async Task GetRandomChoiceAsync_ShouldReturnCorrectChoiceDto_ForDifferentNumbers(int randomNumber, ChoiceEnum expectedChoice)
        {
            // Arrange
            _randomNumberServiceMock.Setup(service => service.GetRandomNumberAsync()).ReturnsAsync(randomNumber);

            // Act
            var result = await _choiceService.GetRandomChoiceAsync();

            // Assert
            result.Id.ShouldBe((int)expectedChoice);
            result.Name.ShouldBe(expectedChoice.ToString());
        }

        [Fact]
        public async Task GetRandomChoiceAsync_ShouldCallRandomNumberServiceOnce()
        {
            // Arrange
            _randomNumberServiceMock.Setup(service => service.GetRandomNumberAsync()).ReturnsAsync(30);

            // Act
            var result = await _choiceService.GetRandomChoiceAsync();

            // Assert
            _randomNumberServiceMock.Verify(service => service.GetRandomNumberAsync(), Times.Once);
        }
    }
}