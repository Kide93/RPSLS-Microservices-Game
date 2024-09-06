using GameLogicService.Business.Models;
using GameLogicService.Business.Requests;
using GameLogicService.Business.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameLogicService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("play")]
        [ProducesResponseType(statusCode: 200, type: typeof(GameResultResponse))]
        public async Task<IActionResult> PlayGame([FromBody] PlayerChoiceRequest playerChoiceRequest,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(playerChoiceRequest, cancellationToken);
            return Ok(response);
        }

        [HttpGet("outcomes")]
        [ProducesResponseType(statusCode: 200, type: typeof(GameOutcome))]
        public async Task<IActionResult> GetGameResults()
        {
            var response = await _mediator.Send(new GameOutcomeRequest());
            return Ok(response);
        }
    }
}
