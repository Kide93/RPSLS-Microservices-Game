using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class PaperState : IChoiceState
    {
        public GameOutcomeEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                RockState => GameOutcomeEnum.Win,
                SpockState => GameOutcomeEnum.Win,
                PaperState => GameOutcomeEnum.Tie,
                _ => GameOutcomeEnum.Lose
            };
        }
    }
}
