using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser 
    [ApiController] //tells api controller if we dont want to use frombody in post
    public class DemoController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator linkGenerator;

        public DemoController(IGameRepository gameRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Get Operations
        /// </summary>
        /// <param name="includeGameSystem"></param>
        /// <returns></returns>
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

        [HttpGet("searchRating/{rating}")]
        public async Task<ActionResult<GamesViewModel[]>> SearchByRating(int rating, bool includeSystemName=false)
        {
            try
            {
                var result = await _gameRepository.SearchRatingGameAsync(rating, includeSystemName);
                if (result == null) return NotFound();
                GamesViewModel[] gamesViewModel = _mapper.Map<GamesViewModel[]>(result);
                return Ok(gamesViewModel);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                //return NotFound("Failed to get games");
                //BadRequest this. etc
            }
        }

        [HttpGet("searchName/{name}")]
        public async Task<ActionResult<GamesViewModel[]>> SearchByGame(string name, bool includeSystemName = false)
        {
            try
            {
                var result = await _gameRepository.SearchNameGameAsync(name, includeSystemName);
                if (result == null) return NotFound();
                GamesViewModel[] gamesViewModel = _mapper.Map<GamesViewModel[]>(result);
                return Ok(gamesViewModel);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                //return NotFound("Failed to get games");
                //BadRequest this. etc
            }
        }

        /// <summary>
        /// Post Operations
        /// </summary>
        /// <param name="gamesViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GamesViewModel>> Post([FromBody] GamesViewModel gamesViewModel)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    //var isvalidGame = _gameRepository.SearchNameGameAsync(gamesViewModel.Name,true);
                    //if (isvalidGame != null)
                    //{
                    //    return BadRequest("Game is already in database");
                    //}

                    //using Automapper
                    var newGame = _mapper.Map<GamesViewModel, Games>(gamesViewModel);

                    if (newGame.CreationDate == DateTime.MinValue)
                    {
                        newGame.CreationDate = DateTime.Now;
                    }
                    _gameRepository.AddEntity(newGame);
                    if (_gameRepository.SaveAll())
                    {
                        //using Automapper
                        return Created($"/api/GameAPI/{newGame.Name}", _mapper.Map<Games, GamesViewModel>(newGame));
                    }
                }
            }

            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

            }
            return BadRequest();

        }

        /// <summary>
        /// Put Operations
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gamesViewModel"></param>
        /// <returns></returns>
        [HttpPut("{name}")]
        public async Task<ActionResult<GamesViewModel>> Put(int name, GamesViewModel gamesViewModel)//string name
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var oldGame =  _gameRepository.GetGameById(name);
                    //var oldGame = _gameRepository.GetGameAsync(name);
                    if (oldGame == null) return NotFound("Not found"); 

                    _mapper.Map(gamesViewModel, oldGame); 

                    if (_gameRepository.SaveAll())
                    {
                        //using Automapper
                        return _mapper.Map<GamesViewModel>(oldGame);    
                    }
                }
            }

            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

            }
            return BadRequest();
        }


    }
}
