 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.Services;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser
    [ApiController] //tells this is an api controller
    [Produces("application/json")] //tells that this controller returns json

    public class GameSystemAPIController : Controller
    { 
        private readonly ILogger<GameAPIController> logger;
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GameSystemAPIController(ILogger<GameAPIController> logger, 
            IGameRepository gameRepository, IMapper mapper)
        { 
            this.logger = logger;
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        [HttpGet]  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //public IEnumerable<Library> Get()
        public ActionResult<IEnumerable<GameSystem>> Get(bool includeItems=true) 
        {
            try
            {
                logger.LogInformation($"game system  api called.");
                //return Ok(gameRepository.GetGameSystems()); 
                var result = gameRepository.GetGameSystems(includeItems);
                return Ok(mapper.Map<IEnumerable<GameSystemAPIViewModel>>(result));

            }
            catch (Exception ex)
            {
                logger.LogInformation($"Failed to get game system: {ex}");
                return BadRequest("Failed to get game system");
            } 
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                logger.LogInformation($"game system  api called.");
                var gameSystem = gameRepository.GetGameSystemsById(id);

                if (gameSystem != null) return Ok(mapper.Map<GameSystem,GameSystemAPIViewModel>(gameSystem));
                else return NotFound(); 

            }
            catch (Exception ex)
            {
                logger.LogInformation($"Failed to get game system: {ex}");
                return BadRequest("Failed to get game system");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]GameSystemAPIViewModel gameSystem)
        {
            //add it to database
            try
            {
                if (ModelState.IsValid)
                {
                    //var newGameSystem = new GameSystem()
                    //{
                    //    CreationDate = gameSystem.CreationDate,
                    //    GameSystemID = gameSystem.GameSystemAPIID,
                    //    SystemName = gameSystem.SystemNameAPI,
                    //    GameLibrary = gameSystem.GameLibrary
                    //};

                    //using Automapper
                    var newGameSystem = mapper.Map<GameSystemAPIViewModel, GameSystem>(gameSystem);

                    if (newGameSystem.CreationDate == DateTime.MinValue)
                    {
                        newGameSystem.CreationDate = DateTime.Now;
                    }
                    gameRepository.AddEntity(newGameSystem);
                    if (gameRepository.SaveAll())
                    {
                        //var vm = new GameSystemAPIViewModel()
                        //{
                        //    CreationDate = newGameSystem.CreationDate,
                        //    GameSystemAPIID = newGameSystem.GameSystemID,
                        //    SystemNameAPI = newGameSystem.SystemName,
                        //    GameLibrary = gameSystem.GameLibrary
                        //};

                        //using Automapper
                        return Created($"/api/GameSystem/{newGameSystem.GameSystemID}", mapper.Map<GameSystem,GameSystemAPIViewModel>(newGameSystem));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to save game system:{ex}");
            }
            return BadRequest("Failed to save game system");
        }

    }
}
