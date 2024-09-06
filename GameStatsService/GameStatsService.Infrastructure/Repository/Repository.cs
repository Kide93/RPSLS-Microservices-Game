using GameStatsService.Business;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using GameStatsService.Infrastructure.Models;
using GameStatsService.Presentation.Mappers;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

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

        public async Task<PaginatedResult<GameResultResponse>> GetGameResultsAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _dbContext.GameResults
                .Where(gameResult => gameResult.UserId == userId)
                .OrderByDescending(gameResult => gameResult.CreatedAt);

            var totalRecords = await query.CountAsync();
            var results = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(gameResult => gameResult.ToResponse())
                .ToListAsync();

            return new PaginatedResult<GameResultResponse>
            {
                Results = results,
                TotalRecords = totalRecords,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<GameResultResponse>> GetGameResultsAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.GameResults
                .OrderByDescending(gameResult => gameResult.CreatedAt);

            var totalRecords = await query.CountAsync();
            var results = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(gameResult => gameResult.ToResponse())
                .ToListAsync();

            return new PaginatedResult<GameResultResponse>
            {
                Results = results,
                TotalRecords = totalRecords,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task ResetAllScoreboard()
        {
            var scoreboards = await _dbContext.Scoreboards.ToListAsync();

            foreach (var scoreboard in scoreboards)
            {
                scoreboard.Wins = 0;
                scoreboard.Losses = 0;
                scoreboard.Ties = 0;
                _dbContext.Scoreboards.Update(scoreboard);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ResetUserScoreboard(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Losses = 0;
            scoreboard.Wins = 0;
            scoreboard.Ties = 0;

            _dbContext.Scoreboards.Update(scoreboard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementLosses()
        {
            var scoreboard = await GetScoreboard();
            scoreboard.Losses++;
            await UpdateScoreboard(scoreboard);
        }

        public async Task IncrementWins()
        {
            var scoreboard = await GetScoreboard();
            scoreboard.Wins++;
            await UpdateScoreboard(scoreboard);
        }

        public async Task IncrementTies()
        {
            var scoreboard = await GetScoreboard();
            scoreboard.Ties++;
            await UpdateScoreboard(scoreboard);
        }

        public async Task IncrementLosses(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Losses++;
            await UpdateScoreboard(scoreboard);
        }

        public async Task IncrementWins(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Wins++;
            await UpdateScoreboard(scoreboard);
        }

        public async Task IncrementTies(string userId)
        {
            var scoreboard = await GetOrCreateScoreboard(userId);
            scoreboard.Ties++;
            await UpdateScoreboard(scoreboard);
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

        private async Task UpdateScoreboard(Scoreboard scoreboard)
        {
            _dbContext.Scoreboards.Update(scoreboard);
            await _dbContext.SaveChangesAsync();
        }
    }
}
