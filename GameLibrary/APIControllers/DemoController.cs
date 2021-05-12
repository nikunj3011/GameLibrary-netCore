using GameLibrary.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser 

    public class DemoController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public DemoController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet] 
        public async Task<IActionResult> GetGame()
        {
            try
            {
                var results = await _gameRepository.GetGamesAsync();
                return Ok(results) ;
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                //return NotFound("Failed to get games");
                //BadRequest this. etc
            }
        }


    }
}
