using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser 

    public class DemoController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public DemoController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [HttpGet] 
        public async Task<IActionResult> GetGames(bool includeGameSystem=false)
        {
            try
            {
                var results = await _gameRepository.GetGamesAsync(includeGameSystem);
                GamesViewModel[] gamesViewModel = _mapper.Map<GamesViewModel[]>(results);
                return Ok(gamesViewModel) ;
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                //return NotFound("Failed to get games");
                //BadRequest this. etc
            }
        }

        [HttpGet("name/{game}")]
        public async Task<ActionResult<GamesViewModel>> Get(string game)
        {
            try
            {
                var result = await _gameRepository.GetGameAsync(game);
                if (result == null) return NotFound();
                GamesViewModel gamesViewModel = _mapper.Map<GamesViewModel>(result);
                return Ok(gamesViewModel);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                //return NotFound("Failed to get games");
                //BadRequest this. etc
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<GamesViewModel>> Get(int id)
        {
            try
            {
                var result = await _gameRepository.GetGameAsync(id);
                if (result == null) return NotFound();
                GamesViewModel gamesViewModel = _mapper.Map<GamesViewModel>(result);
                return Ok(gamesViewModel);
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
