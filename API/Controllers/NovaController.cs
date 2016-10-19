
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Post([FromBody] NovaModel data)
        {
            NovaCommand command = new NovaCommand()
            {
                Id = data.Id,
                Name = data.Name
            };

            var json = JsonConvert.SerializeObject(command, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            var client = new HttpClient(new HttpClientHandler()
            {
                UseDefaultCredentials = true
            });

            var uri = "http://localhost:27499/api/command/dispatch";

            Console.WriteLine("Ready to post!");
            var response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Post success!");
                return Ok("Post success!");
            }

            return new StatusCodeResult(500);
        }
    }
}
