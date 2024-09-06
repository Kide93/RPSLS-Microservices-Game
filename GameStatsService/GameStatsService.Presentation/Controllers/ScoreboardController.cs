using GameStatsService.Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStatsService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScoreboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalScoreboard()
        {
            var globalScoreboard = await _mediator.Send(new GlobalScoreboardRequest());
            return Ok(globalScoreboard);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserScoreboard(string userId)
        {

            return Ok();
        }

        [HttpPost("addresult")]
        public async Task<IActionResult> AddResult([FromBody] GameResultRequest gameResultRequest)
        {
            await _mediator.Send(gameResultRequest);
            return Ok();
        }
    }
}
