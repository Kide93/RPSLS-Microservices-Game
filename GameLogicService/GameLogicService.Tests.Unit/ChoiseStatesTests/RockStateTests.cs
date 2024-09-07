using GameLogicService.Business.Contracts;
using GameLogicService.Business.States;
using Shared.Enums;
using Shouldly;

namespace GameLogicService.Tests.Unit.ChoiseStatesTests
{
    public class RockStateTests
    {
        private readonly IChoiceState _rockState;

        public RockStateTests()
        {
            _rockState = new RockState();
        }

        [Theory]
        [InlineData(typeof(ScissorsState), GameOutcomeEnum.Win)]
        [InlineData(typeof(LizardState), GameOutcomeEnum.Win)]
        [InlineData(typeof(RockState), GameOutcomeEnum.Tie)]
        [InlineData(typeof(PaperState), GameOutcomeEnum.Lose)]
        [InlineData(typeof(SpockState), GameOutcomeEnum.Lose)]
        public void CalculateResult_ShouldReturnExpectedOutcome_WhenRockPlaysAgainstDifferentChoices(Type opponentStateType, GameOutcomeEnum expectedOutcome)
        {
            // Arrange
            var opponentState = (IChoiceState)Activator.CreateInstance(opponentStateType);

            // Act
            var result = _rockState.CalculateResult(opponentState);

            // Assert
            result.ShouldBe(expectedOutcome);
        }
    }
}
