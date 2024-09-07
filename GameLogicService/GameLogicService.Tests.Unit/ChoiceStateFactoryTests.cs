using GameLogicService.Business.Contracts;
using GameLogicService.Business.Implementations;
using GameLogicService.Business.States;
using Shared.Enums;
using Shouldly;

namespace GameLogicService.Tests.Unit
{
    public class ChoiceStateFactoryTests
    {
        private readonly IChoiceStateFactory _choiceStateFactory;

        public ChoiceStateFactoryTests()
        {
            _choiceStateFactory = new ChoiceStateFactory();
        }

        [Theory]
        [InlineData(ChoiceEnum.Rock, typeof(RockState))]
        [InlineData(ChoiceEnum.Paper, typeof(PaperState))]
        [InlineData(ChoiceEnum.Scissors, typeof(ScissorsState))]
        [InlineData(ChoiceEnum.Lizard, typeof(LizardState))]
        [InlineData(ChoiceEnum.Spock, typeof(SpockState))]
        public void GetStateForChoice_ShouldReturnCorrectState_WhenChoiceIsValid(ChoiceEnum choice, Type expectedStateType)
        {
            // Act
            var result = _choiceStateFactory.GetStateForChoice(choice);

            // Assert
            result.ShouldBeOfType(expectedStateType);
        }

        [Fact]
        public void GetStateForChoice_ShouldThrowArgumentException_WhenChoiceIsInvalid()
        {
            // Arrange
            var invalidChoice = (ChoiceEnum)999;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _choiceStateFactory.GetStateForChoice(invalidChoice)
            );

            exception.Message.ShouldBe($"Unknown choice: {invalidChoice}");
        }
    }
}