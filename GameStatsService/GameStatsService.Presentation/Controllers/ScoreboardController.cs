using GameStatsService.Business.Requests;
using GameStatsService.Presentation.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStatsService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IScoreboardService _scoreboardService;
        private readonly IMediator _mediator;

        public ScoreboardController(IScoreboardService scoreboardService, IMediator mediator)
        {
            _scoreboardService = scoreboardService;
            _mediator = mediator;
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

        [HttpPost("addresult")]
        public async Task<IActionResult> AddResult([FromBody] GameResultRequest gameResultRequest)
        {
            await _mediator.Send(gameResultRequest);
            return Ok();
        }
    }
}
