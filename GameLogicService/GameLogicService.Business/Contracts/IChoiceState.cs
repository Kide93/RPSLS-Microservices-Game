using GameLogicService.Business.Models;

namespace GameLogicService.Business.Contracts
{
    public interface IChoiceState
    {
        GameResultEnum CalculateResult(IChoiceState otherChoice);
    }
}
