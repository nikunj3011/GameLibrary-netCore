 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public GameSystemAPIController(ILogger<GameAPIController> logger, IGameRepository gameRepository)
        { 
            this.logger = logger;
            this.gameRepository = gameRepository; 
        }

        [HttpGet]  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //public IEnumerable<Library> Get()
        public ActionResult<IEnumerable<Library>> Get() 
        {
            try
            {
                logger.LogInformation($"game system  api called.");
                return Ok(gameRepository.GetGameSystems()); 

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

                if (gameSystem != null) return Ok(gameSystem);
                else return NotFound(); 

            }
            catch (Exception ex)
            {
                logger.LogInformation($"Failed to get game system: {ex}");
                return BadRequest("Failed to get game system");
            }
        }

    }
}
