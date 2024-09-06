using ChoiceService.Business.Implementations;
using ChoiceService.Business.Models;
using Shouldly;

namespace ChoiceService.Tests.Unit
{
    public class ChoiceProviderTests
    {
        private readonly ChoiceProvider _choiceProvider;

        public ChoiceProviderTests()
        {
            _choiceProvider = new ChoiceProvider();
        }

        [Fact]
        public void GetAllChoices_ShouldReturnAllChoices()
        {
            // Act
            var result = _choiceProvider.GetAllChoices();

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(5);
        }

        [Fact]
        public void GetAllChoices_ShouldReturnCorrectIdsAndNames()
        {
            // Act
            var result = _choiceProvider.GetAllChoices();

            // Assert
            result[0].Id.ShouldBe((int)ChoiceEnum.Rock);
            result[0].Name.ShouldBe(ChoiceEnum.Rock.ToString());

            result[1].Id.ShouldBe((int)ChoiceEnum.Paper);
            result[1].Name.ShouldBe(ChoiceEnum.Paper.ToString());

            result[2].Id.ShouldBe((int)ChoiceEnum.Scissors);
            result[2].Name.ShouldBe(ChoiceEnum.Scissors.ToString());

            result[3].Id.ShouldBe((int)ChoiceEnum.Lizard);
            result[3].Name.ShouldBe(ChoiceEnum.Lizard.ToString());

            result[4].Id.ShouldBe((int)ChoiceEnum.Spock);
            result[4].Name.ShouldBe(ChoiceEnum.Spock.ToString());
        }

        [Fact]
        public void GetAllChoices_ShouldReturnInOrderOfEnumValues()
        {
            // Act
            var result = _choiceProvider.GetAllChoices();

            // Assert
            result[0].Id.ShouldBe((int)ChoiceEnum.Rock);
            result[1].Id.ShouldBe((int)ChoiceEnum.Paper);
            result[2].Id.ShouldBe((int)ChoiceEnum.Scissors);
            result[3].Id.ShouldBe((int)ChoiceEnum.Lizard);
            result[4].Id.ShouldBe((int)ChoiceEnum.Spock);
        }
    }
}
