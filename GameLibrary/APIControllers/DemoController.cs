using Microsoft.AspNetCore.Mvc; 

namespace GameLibrary.Controllers
{
    [Route("api/[Controller]")]  //api path in browser 

    public class DemoController : ControllerBase
    { 
        [HttpGet] 
        public IActionResult GetGame()
        { 
            try
            { 
                return Ok(new { Game = "NFS", System = "PC" }) ;
            }
            catch 
            { 
                return NotFound("Failed to get games");
                //BadRequest this. etc
            }
        }


    }
}
