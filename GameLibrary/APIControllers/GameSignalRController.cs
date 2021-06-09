using AutoMapper;
using Crypto.API;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.SignalR;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{  
    [ApiController] //tells api controller if we dont want to use frombody in post 
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)] 
    [Produces("application/json")] //tells that this controller returns json
    public class GameSignalRController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILogger<GameSignalRController> logger;
        private readonly IHubContext<GameHub> _hub;

        public GameSignalRController(IMapper mapper, LinkGenerator linkGenerator,
            ILogger<GameSignalRController > logger, IHubContext<GameHub> hub)
        {
            _mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            _hub = hub;
        }

        /// <summary>
        /// Get Operations
        /// </summary>
        /// <param name="includeGameSystem"></param>
        /// <returns></returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetGames(bool includeGameSystem = false)
        {
            try
            {
                var timerManager = new CryptoTimer(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
                return Ok(new { Message = "Request Completed" });
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
