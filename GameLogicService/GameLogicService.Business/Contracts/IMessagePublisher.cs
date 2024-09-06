using Shared.Events;

namespace GameLogicService.Business.Contracts
{
    public interface IMessagePublisher
    {
        Task PublishGameResultEvent(GameResultEvent gameResultEvent);
    }
}
