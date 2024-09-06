using ChoiceService.Business.Contracts;
using ChoiceService.Business.Implementations;
using ChoiceService.Business.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace ChoiceService.Tests.Integration
{
    public class ChoiceServiceIntegrationTests
    {
        private readonly Business.Implementations.ChoiceService _choiceService;
        private readonly ChoiceRepository _choiceRepository;

        public ChoiceServiceIntegrationTests()
        {
            _choiceRepository = new ChoiceRepository();

            var loggerMock = new Mock<ILogger<Business.Implementations.ChoiceService>>();
            var randomNumberServiceMock = new Mock<IRandomNumberService>();

            _choiceService = new Business.Implementations.ChoiceService(_choiceRepository, randomNumberServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task GetAllChoicesAsync_ShouldReturnAllChoices_FromRepository()
        {
            // Act
            var result = await _choiceService.GetAllChoicesAsync();

            // Assert
            result.Count.ShouldBe(5);
            result.ShouldContain(choice => choice.Name == ChoiceEnum.Rock.ToString());
            result.ShouldContain(choice => choice.Name == ChoiceEnum.Paper.ToString());
            result.ShouldContain(choice => choice.Name == ChoiceEnum.Scissors.ToString());
            result.ShouldContain(choice => choice.Name == ChoiceEnum.Lizard.ToString());
            result.ShouldContain(choice => choice.Name == ChoiceEnum.Spock.ToString());
        }
    }
}