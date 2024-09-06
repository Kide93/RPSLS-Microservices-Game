using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class SpockState : IChoiceState
    {
        public GameOutcomeEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                RockState => GameOutcomeEnum.Win,
                ScissorsState => GameOutcomeEnum.Win,
                SpockState => GameOutcomeEnum.Tie,
                _ => GameOutcomeEnum.Lose
            };
        }
    }
}
