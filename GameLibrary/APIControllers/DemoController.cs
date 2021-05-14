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
        public async Task<IActionResult> GetGame()
        {
            try
            {
                var results = await _gameRepository.GetGamesAsync();
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


    }
}
