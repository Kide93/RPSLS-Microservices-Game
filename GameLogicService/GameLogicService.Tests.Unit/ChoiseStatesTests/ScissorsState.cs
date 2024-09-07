using GameLogicService.Business.Contracts;
using GameLogicService.Business.States;
using Shared.Enums;
using Shouldly;

namespace GameLogicService.Tests.Unit.ChoiseStatesTests
{
    public class ScissorsStateTests
    {
        private readonly IChoiceState _scissorsState;

        public ScissorsStateTests()
        {
            _scissorsState = new ScissorsState();
        }

        [Theory]
        [InlineData(typeof(PaperState), GameOutcomeEnum.Win)]
        [InlineData(typeof(LizardState), GameOutcomeEnum.Win)]
        [InlineData(typeof(ScissorsState), GameOutcomeEnum.Tie)]
        [InlineData(typeof(RockState), GameOutcomeEnum.Lose)]
        [InlineData(typeof(SpockState), GameOutcomeEnum.Lose)]
        public void CalculateResult_ShouldReturnExpectedOutcome_WhenScissorsPlaysAgainstDifferentChoices(Type opponentStateType, GameOutcomeEnum expectedOutcome)
        {
            // Arrange
            var opponentState = (IChoiceState)Activator.CreateInstance(opponentStateType);

            // Act
            var result = _scissorsState.CalculateResult(opponentState);

            // Assert
            result.ShouldBe(expectedOutcome);
        }
    }
}
