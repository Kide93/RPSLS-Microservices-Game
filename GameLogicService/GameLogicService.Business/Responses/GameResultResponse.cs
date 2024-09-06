using Shared.Enums;

namespace GameLogicService.Business.Responses
{
    public class GameResultResponse
    {
        public string Results { get; set; }
        public int PlayerChoice { get; set; }
        public int ComputerChoice { get; set; }

        public GameResultResponse(ChoiceEnum playerChoice, ChoiceEnum computerChoice, GameOutcomeEnum outcome)
        {
            Results = outcome.ToString();
            PlayerChoice = (int)playerChoice;
            ComputerChoice = (int)computerChoice;
        }
    }
}
