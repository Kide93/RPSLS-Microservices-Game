using GameStatsService.Presentation.Contracts;
using GameStatsService.Presentation.Models;
using Shared.Enums;
using Shared.Events;

namespace GameStatsService.Presentation.Implementations
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly Dictionary<string, Scoreboard> _userScoreboards = new();
        private readonly Scoreboard _globalScoreboard = new();

        public Task UpdateScoreboard(GameResultEvent gameResultEvent)
        {
            UpdateGlobalScoreboard(gameResultEvent);

            if (!_userScoreboards.ContainsKey(gameResultEvent.UserId))
            {
                _userScoreboards[gameResultEvent.UserId] = new Scoreboard();
            }

            UpdateUserScoreboard(gameResultEvent.UserId, gameResultEvent);

            return Task.CompletedTask;
        }

        private void UpdateGlobalScoreboard(GameResultEvent gameResultEvent)
        {
            switch (gameResultEvent.Result)
            {
                case GameResultEnum.Win:
                    _globalScoreboard.Wins++;
                    break;
                case GameResultEnum.Lose:
                    _globalScoreboard.Losses++;
                    break;
                case GameResultEnum.Tie:
                    _globalScoreboard.Ties++;
                    break;
            }
        }

        private void UpdateUserScoreboard(string userId, GameResultEvent gameResultEvent)
        {
            var scoreboard = _userScoreboards[userId];
            switch (gameResultEvent.Result)
            {
                case GameResultEnum.Win:
                    scoreboard.Wins++;
                    break;
                case GameResultEnum.Lose:
                    scoreboard.Losses++;
                    break;
                case GameResultEnum.Tie:
                    scoreboard.Ties++;
                    break;
            }
        }

        public Task<Scoreboard> GetGlobalScoreboard()
        {
            return Task.FromResult(_globalScoreboard);
        }

        public Task<Scoreboard> GetUserScoreboard(string userId)
        {
            if (_userScoreboards.ContainsKey(userId))
            {
                return Task.FromResult(_userScoreboards[userId]);
            }
            return Task.FromResult<Scoreboard>(null);
        }
    }
}
