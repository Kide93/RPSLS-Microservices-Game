using ChoiceService.Business.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace ChoiceService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(RandomChoiceResponse))]
        public async Task<IActionResult> GetRandomChoice()
        {
            var randomChoice = await _mediator.Send(new GetRandomChoiceRequest());
            return Ok(randomChoice);
        }
    }
}