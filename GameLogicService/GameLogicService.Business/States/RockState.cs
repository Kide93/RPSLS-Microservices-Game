using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class RockState : IChoiceState
    {
        public GameOutcomeEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                ScissorsState => GameOutcomeEnum.Win,
                LizardState => GameOutcomeEnum.Win,
                RockState => GameOutcomeEnum.Tie,
                _ => GameOutcomeEnum.Lose
            };
        }
    }
}
