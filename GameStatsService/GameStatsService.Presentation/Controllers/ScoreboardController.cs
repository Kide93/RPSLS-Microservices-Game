using GameStatsService.Business.Requests;
using GameStatsService.Business.Responses;
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
        [ProducesResponseType(statusCode: 200, type: typeof(ScoreboardResponse))]
        public async Task<IActionResult> GetGlobalScoreboard()
        {
            var globalScoreboard = await _mediator.Send(new GlobalScoreboardRequest());
            return Ok(globalScoreboard);
        }

        [HttpGet("global/history")]
        [ProducesResponseType(statusCode: 200, type: typeof(GlobalScoreboardHistoryRequest))]
        public async Task<IActionResult> GetGlobalGameResultsHistory(int pageNumber = 1, int pageSize = 10)
        {
            var globalScoreboard = await _mediator.Send(new GlobalScoreboardHistoryRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return Ok(globalScoreboard);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(statusCode: 200, type: typeof(ScoreboardResponse))]
        public async Task<IActionResult> GetUserScoreboard(string userId)
        {
            var globalScoreboard = await _mediator.Send(new UserScoreboardRequest { UserId = userId });
            return Ok(globalScoreboard);
        }

        [HttpGet("user/{userId}/history")]
        [ProducesResponseType(statusCode: 200, type: typeof(ScoreboardHistoryResponse))]
        public async Task<IActionResult> GetUserGameResultsHistory(string userId, int pageNumber = 1, int pageSize = 10)
        {
            var globalScoreboard = await _mediator.Send(new UserScoreboardHistoryRequest
            {
                UserId = userId,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return Ok(globalScoreboard);
        }

        [HttpPost("reset/global")]
        [ProducesResponseType(statusCode: 200)]
        public async Task<IActionResult> ResetGlobalScoreboard()
        {
            await _mediator.Send(new ResetScoreboardRequest());
            return Ok();
        }

        [HttpPost("reset/user/{userId}")]
        public async Task<IActionResult> ResetUserScoreboard(string userId)
        {
            await _mediator.Send(new ResetUserScoreboardRequest { UserId = userId });
            return Ok();
        }
    }
}
