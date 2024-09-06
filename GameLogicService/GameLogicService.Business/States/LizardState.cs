using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class LizardState : IChoiceState
    {
        public GameOutcomeEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                PaperState => GameOutcomeEnum.Win,
                SpockState => GameOutcomeEnum.Win,
                LizardState => GameOutcomeEnum.Tie,
                _ => GameOutcomeEnum.Lose
            };
        }
    }
}
