using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    [Route("/api/Games/{gameid}/GameSystem")]
    public class GameswithSystemAPIController: Controller
    {
        private readonly ILogger<GameAPIController> logger;
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public GameswithSystemAPIController(ILogger<GameAPIController> logger,
            IGameRepository gameRepository, IMapper mapper)
        {
            this.logger = logger;
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get (int gameid)
        {
            var game = gameRepository.GetGameById(gameid);
            if (game != null) return Ok(mapper.Map<GameSystem,
                GameSystemAPIViewModel>( game.GameSystems));
            return NotFound();
        }
    }
}
