using GameStatsService.Infrastructure.Models;

namespace GameStatsService.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(this GameStatsDbContext context)
        {
            if (!context.Scoreboards.Any(s => s.IsGlobal))
            {
                context.Scoreboards.Add(new Scoreboard
                {
                    Id = 1,
                    IsGlobal = true,
                    Wins = 0,
                    Losses = 0,
                    Ties = 0
                });
                context.SaveChanges();
            }
        }
    }
}
