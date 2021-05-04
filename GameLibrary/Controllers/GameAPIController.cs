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
    [Route("api/[Controller]")] 
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
        public IEnumerable<Library> Get()
        {
            //throw new InvalidOperationException();
            logger.LogInformation("game api called");
            return gameRepository.GetGameLibraries();
        }

    }
}
