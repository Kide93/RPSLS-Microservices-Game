using ChoiceService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace ChoiceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoiceController : ControllerBase
    {
        private readonly IChoiceService _choiceService;

        public ChoiceController(IChoiceService choiceService)
        {
            _choiceService = choiceService;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(RandomChoiceResponseDto))]
        public async Task<IActionResult> GetRandomChoice()
        {
            var randomChoice = await _choiceService.GetRandomChoiceAsync();
            return Ok(randomChoice);
        }
    }
}