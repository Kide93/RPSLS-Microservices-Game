using ChoiceService.Business.Requests;
using ChoiceService.Business.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChoiceService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(List<ChoiceResponse>))]
        public async Task<IActionResult> GetChoices()
        {
            var choices = await _mediator.Send(new GetChoicesRequest());
            return Ok(choices.Choices);
        }
    }
}
