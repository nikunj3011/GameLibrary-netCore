using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("/api/Games/{name}/GameSystem")]
    [ApiController] 
    public class GameswithSystemAPIController: Controller
    {
        private readonly ILogger<GameAPIController> logger;
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public GameswithSystemAPIController(ILogger<GameAPIController> logger,
            IGameRepository gameRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.logger = logger;
            this.gameRepository = gameRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<GameSystemAPIViewModel[]>> Get (string name)
        {
            try
            {
                var game = await gameRepository.GetGameSystemsByGameName(name);
                if (game != null) return Ok(mapper.Map<GameSystem[],
                    GameSystemAPIViewModel[]>(game));
                return NotFound();
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
