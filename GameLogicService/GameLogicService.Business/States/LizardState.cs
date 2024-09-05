using GameLogicService.Business.Contracts;
using GameLogicService.Business.Models;

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
