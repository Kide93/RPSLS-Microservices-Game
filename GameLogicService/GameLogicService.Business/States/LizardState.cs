using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class LizardState : IChoiceState
    {
        public GameResultEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                PaperState => GameResultEnum.Win,
                SpockState => GameResultEnum.Win,
                LizardState => GameResultEnum.Tie,
                _ => GameResultEnum.Lose
            };
        }
    }
}
