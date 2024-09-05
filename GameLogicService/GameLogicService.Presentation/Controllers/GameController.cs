using GameLogicService.Business.Models;
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
        public async Task<IActionResult> PlayGame([FromBody] PlayerChoiceRequest playerChoiceRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(playerChoiceRequest, cancellationToken);
            return Ok(result);
        }
    }
}
