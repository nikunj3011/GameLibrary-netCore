 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.Services;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController] //tells this is an api controller
    [Produces("application/json")] //tells that this controller returns json

    public class GameAPIController : Controller
    { 
        private readonly ILogger<GameAPIController> logger;
        private readonly IGameRepository gameRepository; 

        public GameAPIController(ILogger<GameAPIController> logger, IGameRepository gameRepository)
        { 
            this.logger = logger;
            this.gameRepository = gameRepository; 
        }

        [HttpGet]  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //public IEnumerable<Library> Get()
        public ActionResult<IEnumerable<Games>> Get() 
        {
            try
            {
                logger.LogInformation($"game api called.");
                return Ok(gameRepository.GetGameLibraries()); 

            }
            catch (Exception ex)
            {
                logger.LogInformation($"Failed to get games: {ex}");
                return BadRequest("Failed to get games");
            } 
        }

    }
}
