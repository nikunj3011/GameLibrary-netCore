using Microsoft.AspNetCore.Mvc; 

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser 

    public class DemoController : ControllerBase
    { 
        [HttpGet] 
        public object GetGame()
        { 
            try
            { 
                return new { Game = "NFS", System = "PC" };
            }
            catch 
            { 
                return BadRequest("Failed to get games");
            }
        }


    }
}
