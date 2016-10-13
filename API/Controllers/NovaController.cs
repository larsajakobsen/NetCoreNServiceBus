
using Microsoft.AspNetCore.Mvc;
using Novanet.NetCoreNServiceBus.Handler.Models;

namespace Novanet.NetCoreNServiceBus.Handler.Controllers
{
    [Route("api/[controller]")]
    public class NovaController : Controller
    {
        public IActionResult Get()
        {
            return Ok("Net!");
        }

        [HttpPost]
        public IActionResult Post([FromBody] NovaData data)
        {
            return Ok("Post success!");
        }
    }
}
