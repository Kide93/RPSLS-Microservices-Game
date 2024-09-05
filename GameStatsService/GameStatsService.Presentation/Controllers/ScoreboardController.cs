using GameStatsService.Presentation.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStatsService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IScoreboardService _scoreboardService;

        public ScoreboardController(IScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalScoreboard()
        {
            var scoreboard = await _scoreboardService.GetGlobalScoreboard();
            return Ok(scoreboard);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserScoreboard(string userId)
        {
            var scoreboard = await _scoreboardService.GetUserScoreboard(userId);
            if (scoreboard == null)
            {
                return NotFound();
            }
            return Ok(scoreboard);
        }
    }
}
