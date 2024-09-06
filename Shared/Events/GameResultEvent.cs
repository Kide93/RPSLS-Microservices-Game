using Shared.Enums;

namespace Shared.Events
{
    public class GameResultEvent
    {
        public string UserId { get; set; }
        public ChoiceEnum PlayerChoice { get; set; }
        public ChoiceEnum ComputerChoice { get; set; }
        public GameOutcomeEnum Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
