
using Microsoft.AspNetCore.Mvc;
using Novanet.NetCoreNServiceBus.Contracts;
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
            NovaCommand command = new NovaCommand();

            return Ok("Post success!");
        }
    }
}
