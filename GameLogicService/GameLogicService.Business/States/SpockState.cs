﻿using GameLogicService.Business.Contracts;
using Shared.Enums;

namespace GameLogicService.Business.States
{
    public class SpockState : IChoiceState
    {
        public GameResultEnum CalculateResult(IChoiceState otherChoice)
        {
            return otherChoice switch
            {
                RockState => GameResultEnum.Win,
                ScissorsState => GameResultEnum.Win,
                SpockState => GameResultEnum.Tie,
                _ => GameResultEnum.Lose
            };
        }
    }
}
