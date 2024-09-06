using Shared.Enums;

namespace GameLogicService.Business.Contracts
{
    public interface IChoiceState
    {
        GameOutcomeEnum CalculateResult(IChoiceState otherChoice);
    }
}
