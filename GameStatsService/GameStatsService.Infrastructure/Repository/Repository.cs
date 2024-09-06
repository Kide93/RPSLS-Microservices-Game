using GameStatsService.Business;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using GameStatsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStatsService.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly GameStatsDbContext _dbContext;

        public Repository(GameStatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddResult(GameResultRequest gameResultRequest, CancellationToken cancellationToken)
        {
            var gameResult = new GameResult
            {
                UserChoice = (int)gameResultRequest.PlayerChoice,
                ComputerChoice = (int)gameResultRequest.ComputerChoice,
                UserId = gameResultRequest.UserId,
                CreatedAt = DateTime.UtcNow,
                Result = (int)gameResultRequest.Result
            };

            _dbContext.GameResults.Add(gameResult);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<GlobalScoreboardResponse> GetGlobalScoreboard()
        {
            var scoreboard = await _dbContext.Scoreboards.FirstAsync(_ => _.IsGlobal);
            var scoreboardDto = new GlobalScoreboardResponse
            {
                Losses = scoreboard.Losses,
                Ties = scoreboard.Ties,
                Wins = scoreboard.Wins
            };

            return scoreboardDto;
        }

        public async Task ResetScoreboard()
        {
            var scoreboard = await _dbContext.Scoreboards.FirstAsync(_ => _.IsGlobal);
            scoreboard.Losses = 0;
            scoreboard.Ties = 0;
            scoreboard.Wins = 0;
            _dbContext.Scoreboards.Update(scoreboard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementLosses()
        {
            var scoreboard = await _dbContext.Scoreboards.FirstAsync(_ => _.IsGlobal);
            scoreboard.Losses++;
            _dbContext.Scoreboards.Update(scoreboard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementWins()
        {
            var scoreboard = await _dbContext.Scoreboards.FirstAsync(_ => _.IsGlobal);
            scoreboard.Wins++;
            _dbContext.Scoreboards.Update(scoreboard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementTies()
        {
            var scoreboard = await _dbContext.Scoreboards.FirstAsync(_ => _.IsGlobal);
            scoreboard.Ties++;
            _dbContext.Scoreboards.Update(scoreboard);
            await _dbContext.SaveChangesAsync();
        }
    }
}
