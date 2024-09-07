using GameLogicService.Business.Contracts;
using GameLogicService.Business.States;
using Shared.Enums;
using Shouldly;

namespace GameLogicService.Tests.Unit.ChoiseStatesTests
{
    public class SpockStateTests
    {
        private readonly IChoiceState _spockState;

        public SpockStateTests()
        {
            _spockState = new SpockState();
        }

        [Theory]
        [InlineData(typeof(RockState), GameOutcomeEnum.Win)]
        [InlineData(typeof(ScissorsState), GameOutcomeEnum.Win)]
        [InlineData(typeof(SpockState), GameOutcomeEnum.Tie)]
        [InlineData(typeof(PaperState), GameOutcomeEnum.Lose)]
        [InlineData(typeof(LizardState), GameOutcomeEnum.Lose)]
        public void CalculateResult_ShouldReturnExpectedOutcome_WhenSpockPlaysAgainstDifferentChoices(Type opponentStateType, GameOutcomeEnum expectedOutcome)
        {
            // Arrange
            var opponentState = (IChoiceState)Activator.CreateInstance(opponentStateType);

            // Act
            var result = _spockState.CalculateResult(opponentState);

            // Assert
            result.ShouldBe(expectedOutcome);
        }
    }
}
