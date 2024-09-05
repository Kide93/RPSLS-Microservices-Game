using GameLogicService.Business.Contracts;
using GameLogicService.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace GameLogicService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameLogicService _gameLogicService;


        public GameController(IGameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [HttpPost("play")]
        public async Task<IActionResult> PlayGame([FromBody] GameRequestDto gameRequest)
        {
            // TODO: move validation from here
            if (!Enum.IsDefined(typeof(ChoiceEnum), gameRequest.PlayerChoice))
            {
                return BadRequest("Invalid player choice.");
            }

            var gameResult = await _gameLogicService.Play(gameRequest.PlayerChoice);

            return Ok(gameResult);
        }
    }
}
