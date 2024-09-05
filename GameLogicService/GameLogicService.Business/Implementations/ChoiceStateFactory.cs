using GameLogicService.Business.Contracts;
using GameLogicService.Business.States;
using Shared.Enums;

namespace GameLogicService.Business.Implementations
{
    public class ChoiceStateFactory : IChoiceStateFactory
    {
        public IChoiceState GetStateForChoice(ChoiceEnum choice)
        {
            return choice switch
            {
                ChoiceEnum.Rock => new RockState(),
                ChoiceEnum.Paper => new PaperState(),
                ChoiceEnum.Scissors => new ScissorsState(),
                ChoiceEnum.Lizard => new LizardState(),
                ChoiceEnum.Spock => new SpockState(),
                _ => throw new ArgumentException($"Unknown choice: {choice}")
            };
        }
    }
}
