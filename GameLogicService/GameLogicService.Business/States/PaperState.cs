using GameLogicService.Business.Contracts;
using GameLogicService.Business.Models;

namespace GameLogicService.Business.States
{
    public class PaperState : IChoiceState
    {
        public GameResultEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                RockState => GameResultEnum.Win,
                SpockState => GameResultEnum.Win,
                PaperState => GameResultEnum.Tie,
                _ => GameResultEnum.Lose
            };
        }
    }
}
