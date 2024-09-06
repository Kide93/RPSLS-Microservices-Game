namespace GameStatsService.Infrastructure.Models
{
    public class Scoreboard
    {
        public int Id { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public string UserId { get; set; }
        public bool IsGlobal { get; set; }
    }
}
