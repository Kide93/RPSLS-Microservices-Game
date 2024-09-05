namespace GameStatsService.Infrastructure.Models
{
    public class Results
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int UserChoice { get; set; }
        public int ComputerChoice { get; set; }
        public int Result { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
