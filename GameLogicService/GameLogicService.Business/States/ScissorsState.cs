﻿using GameLogicService.Business.Contracts;
using GameLogicService.Business.Models;

namespace GameLogicService.Business.States
{
    public class ScissorsState : IChoiceState
    {
        public GameResultEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                PaperState => GameResultEnum.Win,
                LizardState => GameResultEnum.Win,
                ScissorsState => GameResultEnum.Tie,
                _ => GameResultEnum.Lose
            };
        }
    }
}
