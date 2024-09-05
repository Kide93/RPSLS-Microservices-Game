namespace GameStatsService.Infrastructure.Models
{
    public class Scoreboard
    {
        public int Id { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}
