using GameStatsService.Business.Repositories;
using GameStatsService.Business.Responses;
using GameStatsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStatsService.Infrastructure.Repository
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private readonly GameStatsDbContext _dbContext;

        public ScoreboardRepository(GameStatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ScoreboardResponse> GetGlobalScoreboard()
        {
            var scoreboard = await GetScoreboard();
            var scoreboardResponse = new ScoreboardResponse
            {
                Losses = scoreboard.Losses,
                Ties = scoreboard.Ties,
                Wins = scoreboard.Wins
            };

            return scoreboardResponse;
        }

        public async Task<ScoreboardResponse> GetUserScoreboard(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);

            if (scoreboard == null)
            {
                return new ScoreboardResponse();
            }

            var scoreboardResponse = new ScoreboardResponse
            {
                Losses = scoreboard.Losses,
                Ties = scoreboard.Ties,
                Wins = scoreboard.Wins
            };

            return scoreboardResponse;
        }

        public async Task ResetAllScoreboard()
        {
            var scoreboards = await _dbContext.Scoreboards.ToListAsync();

            foreach (var scoreboard in scoreboards)
            {
                scoreboard.Wins = 0;
                scoreboard.Losses = 0;
                scoreboard.Ties = 0;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ResetUserScoreboard(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Losses = 0;
            scoreboard.Wins = 0;
            scoreboard.Ties = 0;

            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementLosses()
        {
            var scoreboard = await GetScoreboard();
            scoreboard.Losses++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementWins()
        {
            var scoreboard = await GetScoreboard();
            scoreboard.Wins++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementTies()
        {
            var scoreboard = await GetScoreboard();
            scoreboard.Ties++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementLosses(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Losses++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementWins(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Wins++;
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementTies(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Ties++;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Scoreboard> GetScoreboard()
            => await _dbContext.Scoreboards.FirstAsync(_ => _.IsGlobal);

        private async Task<Scoreboard> GetOrCreateScoreboard(string userId)
        {
            var scoreboard = await _dbContext.Scoreboards
                .Where(_ => string.Equals(userId, _.UserId))
                .FirstOrDefaultAsync();

            if (scoreboard == null)
            {
                scoreboard = new Scoreboard
                {
                    UserId = userId,
                    IsGlobal = false
                };

                _dbContext.Scoreboards.Add(scoreboard);
            }

            return scoreboard;
        }
    }
}
