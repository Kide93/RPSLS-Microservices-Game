using GameLogicService.Business.Contracts;
using GameLogicService.Business.States;
using Shared.Enums;
using Shouldly;

namespace GameLogicService.Tests.Unit.ChoiseStatesTests
{
    public class LizardStateTests
    {
        private readonly IChoiceState _lizardState;

        public LizardStateTests()
        {
            _lizardState = new LizardState();
        }

        [Theory]
        [InlineData(typeof(PaperState), GameOutcomeEnum.Win)]
        [InlineData(typeof(SpockState), GameOutcomeEnum.Win)]
        [InlineData(typeof(LizardState), GameOutcomeEnum.Tie)]
        [InlineData(typeof(RockState), GameOutcomeEnum.Lose)]
        [InlineData(typeof(ScissorsState), GameOutcomeEnum.Lose)]
        public void CalculateResult_ShouldReturnExpectedOutcome_WhenLizardPlaysAgainstDifferentChoices(Type opponentStateType, GameOutcomeEnum expectedOutcome)
        {
            // Arrange
            var opponentState = (IChoiceState)Activator.CreateInstance(opponentStateType);

            // Act
            var result = _lizardState.CalculateResult(opponentState);

            // Assert
            result.ShouldBe(expectedOutcome);
        }
    }
}
