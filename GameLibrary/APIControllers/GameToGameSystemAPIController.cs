using GameLibrary.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.APIControllers
{
    [Route("api/[Controller]")]  //api path in browser 
    [ApiController] //tells api controller if we dont want to use frombody in post 
    [Produces("application/json")] //tells that this controller returns json
    public class GameToGameSystemAPIController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        public GameToGameSystemAPIController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpPost("postToGameSystemAPI")]
        public async Task<IActionResult> Post(Games gameSystem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGame = new Games()
                    {
                        CreationDate = DateTime.Now,
                        GameSystemID = 1,
                        Description = gameSystem.Description,
                        DiscType = gameSystem.DiscType,
                        Name = gameSystem.Name,
                        Rating = gameSystem.Rating
                    };

                    StringContent content = new StringContent(JsonConvert.SerializeObject(newGame), Encoding.UTF8, "application/json");
                    var x = content.ReadAsStringAsync();
                    HttpResponseMessage response;
                    var client = _clientFactory.CreateClient("gameService");
                    response = await client.PostAsync($"/api/s/GameswithSystemAPI", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest($"something wrong");
                    }
                    var data = await response.Content.ReadAsStringAsync();
                    var element = JsonConvert.DeserializeObject<Games>(data);

                    return Ok(element);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
            }
            return BadRequest("Failed to save game system");
        }


    }
}
