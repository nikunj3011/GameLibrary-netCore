 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.Services;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController] //tells this is an api controller
    [Produces("application/json")] //tells that this controller returns json

    public class GameSystemAPIController : Controller
    { 
        private readonly ILogger<GameSystemAPIController> logger;
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;
        private readonly UserManager<StoreUser> userManager;

        public GameSystemAPIController(ILogger<GameSystemAPIController> logger, 
            IGameRepository gameRepository, IMapper mapper, UserManager<StoreUser> userManager)
        { 
            this.logger = logger;
            this.gameRepository = gameRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //public IEnumerable<Library> Get()
        public ActionResult<IEnumerable<GameSystem>> Get(bool includeItems=false) 
        {
            try
            {
                logger.LogInformation($"game system  api called.");
                //return Ok(gameRepository.GetGameSystems()); 
                var result = gameRepository.GetGameSystems(includeItems);
                return Ok(result);

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

                if (gameSystem != null) return Ok(mapper.Map<GameSystem, GameSystemAPIViewModel>(gameSystem));
                else return NotFound(); 

            }
            catch (Exception ex)
            {
                logger.LogInformation($"Failed to get game system: {ex}");
                return BadRequest("Failed to get game system");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameSystemAPIViewModel gameSystem)
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
                    //var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                    //newGameSystem.user = currentUser;
                    gameRepository.AddEntity(newGameSystem);
                    if (await gameRepository.SaveAll())
                    {
                        //var vm = new GameSystemAPIViewModel()
                        //{
                        //    CreationDate = newGameSystem.CreationDate,
                        //    GameSystemAPIID = newGameSystem.GameSystemID,
                        //    SystemNameAPI = newGameSystem.SystemName,
                        //    GameLibrary = gameSystem.GameLibrary
                        //};

                        //using Automapper
                        return Created($"/api/GameSystem/{newGameSystem.GameSystemID}", mapper.Map<GameSystem, GameSystemAPIViewModel>(newGameSystem));
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
