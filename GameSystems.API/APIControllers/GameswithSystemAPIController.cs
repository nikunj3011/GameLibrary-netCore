﻿using AutoMapper;
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
    [Route("/api/s/[Controller]")]
    [ApiController] 
    public class GameswithSystemAPIController: Controller
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public GameswithSystemAPIController(
            IGameRepository gameRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]///api/s/GameswithSystemAPI/test
        public async Task<IActionResult> Test()
        {
            //add it to database
            try
            {
                return Ok("working");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to save game system");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Games gameSystem)
        {
            //add it to database
            try
            {
                if (ModelState.IsValid)
                {
                    gameSystem.Name = "Name changed in other api";
                    return Ok(gameSystem);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to save game system");
            }
        }
    }
}
