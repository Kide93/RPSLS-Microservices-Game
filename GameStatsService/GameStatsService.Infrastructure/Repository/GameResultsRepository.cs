using GameStatsService.Business.Repositories;
using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
using GameStatsService.Infrastructure.Mappers;
using GameStatsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace GameStatsService.Infrastructure.Repository
{
    public class GameResultsRepository : IGameResultsRepository
    {
        private readonly GameStatsDbContext _dbContext;

        public GameResultsRepository(GameStatsDbContext dbContext)
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
    }
}
