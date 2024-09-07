using GameLogicService.Business.Contracts;
using GameLogicService.Business.States;
using Shared.Enums;
using Shouldly;

namespace GameLogicService.Tests.Unit.ChoiseStatesTests
{
    public class PaperStateTests
    {
        private readonly IChoiceState _paperState;

        public PaperStateTests()
        {
            _paperState = new PaperState();
        }

        [Theory]
        [InlineData(typeof(RockState), GameOutcomeEnum.Win)]
        [InlineData(typeof(SpockState), GameOutcomeEnum.Win)]
        [InlineData(typeof(PaperState), GameOutcomeEnum.Tie)]
        [InlineData(typeof(LizardState), GameOutcomeEnum.Lose)]
        [InlineData(typeof(ScissorsState), GameOutcomeEnum.Lose)]
        public void CalculateResult_ShouldReturnExpectedOutcome_WhenPaperPlaysAgainstDifferentChoices(Type opponentStateType, GameOutcomeEnum expectedOutcome)
        {
            // Arrange
            var opponentState = (IChoiceState)Activator.CreateInstance(opponentStateType);

            // Act
            var result = _paperState.CalculateResult(opponentState);

            // Assert
            result.ShouldBe(expectedOutcome);
        }
    }
}
