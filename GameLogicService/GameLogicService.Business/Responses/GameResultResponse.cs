using Shared.Enums;

namespace GameLogicService.Business.Responses
{
    public class GameResultResponse
    {
        public int Result { get; set; }
        public int PlayerChoice { get; set; }
        public int ComputerChoice { get; set; }

        public GameResultResponse(ChoiceEnum playerChoice, ChoiceEnum computerChoice, GameOutcomeEnum outcome)
        {
            Result = (int)outcome;
            PlayerChoice = (int)playerChoice;
            ComputerChoice = (int)computerChoice;
        }
    }
}
