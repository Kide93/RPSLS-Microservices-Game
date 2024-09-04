using ChoiceService.Exceptions;
using ChoiceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChoiceService.Controllers
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
        public async Task<IActionResult> GetChoices()
        {
            var choices = await _choiceService.GetAllChoicesAsync();
            return Ok(choices);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomChoice()
        {
            try
            {
                var randomChoice = await _choiceService.GetRandomChoiceAsync();
                return Ok(randomChoice);
            }
            catch (ExternalServiceException ex)
            {
                return StatusCode(503, new { message = ex.Message });
            }
            catch (DeserializationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", detail = ex.Message });
            }
        }
    }
}