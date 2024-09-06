using Shared.Enums;

namespace GameStatsService.Business.Responses
{
    public class GameResultResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int UserChoice { get; set; }
        public int ComputerChoice { get; set; }
        public GameOutcomeEnum Result { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
