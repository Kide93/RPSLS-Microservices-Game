using Shared.Enums;

namespace GameLogicService.Business.Models
{
    public class GameResultResponse
    {
        public int Result { get; set; }
        public int PlayerChoice { get; set; }
        public int ComputerChoice { get; set; }

        public GameResultResponse(ChoiceEnum playerChoice, ChoiceEnum computerChoice, GameResultEnum result)
        {
            Result = (int)result;
            PlayerChoice = (int)playerChoice;
            ComputerChoice = (int)computerChoice;
        }
    }
}
