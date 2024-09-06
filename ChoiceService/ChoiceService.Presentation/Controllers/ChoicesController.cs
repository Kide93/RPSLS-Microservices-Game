using ChoiceService.Business.Contracts;
using ChoiceService.Business.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ChoiceService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoicesController : ControllerBase
    {
        private readonly IChoiceService _choiceService;

        public ChoicesController(IChoiceService choiceService)
        {
            _choiceService = choiceService;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(List<ChoiceDto>))]
        public async Task<IActionResult> GetChoices()
        {
            var choices = await _choiceService.GetAllChoicesAsync();
            return Ok(choices);
        }
    }
}
