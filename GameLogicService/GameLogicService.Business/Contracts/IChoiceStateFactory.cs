using Shared.Enums;

namespace GameLogicService.Business.Contracts
{
    public interface IChoiceStateFactory
    {
        IChoiceState GetStateForChoice(ChoiceEnum choice);
    }
}
