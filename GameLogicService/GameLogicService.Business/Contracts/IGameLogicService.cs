using GameLogicService.Business.Models;
using Shared.Enums;

namespace GameLogicService.Business.Contracts
{
    public interface IGameLogicService
    {
        Task<GameResult> Play(ChoiceEnum playerChoice);
    }
}
