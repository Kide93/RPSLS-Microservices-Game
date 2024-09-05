using Shared.Enums;

namespace GameLogicService.Business.Contracts
{
    public interface IChoiceState
    {
        GameResultEnum CalculateResult(IChoiceState otherChoice);
    }
}
