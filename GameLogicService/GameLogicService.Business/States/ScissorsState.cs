using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class ScissorsState : IChoiceState
    {
        public GameOutcomeEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                PaperState => GameOutcomeEnum.Win,
                LizardState => GameOutcomeEnum.Win,
                ScissorsState => GameOutcomeEnum.Tie,
                _ => GameOutcomeEnum.Lose
            };
        }
    }
}
