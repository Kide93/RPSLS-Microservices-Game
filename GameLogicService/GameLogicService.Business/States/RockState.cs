using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class RockState : IChoiceState
    {
        public GameResultEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                ScissorsState => GameResultEnum.Win,
                LizardState => GameResultEnum.Win,
                RockState => GameResultEnum.Tie,
                _ => GameResultEnum.Lose
            };
        }
    }
}
