
using System;
using Microsoft.AspNetCore.Mvc;
using Novanet.NetCoreNServiceBus.Contracts;
using Novanet.NetCoreNServiceBus.Handler.Models;
using NServiceBus;

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
        public IActionResult Post([FromBody] NovaModel data)
        {
            NovaCommand command = new NovaCommand()
            {
                Id = data.Id,
                Name = data.Name
            };

            var busConfiguration = GetBusConfig();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                bus.Send("Novanet.NetCoreNServiceBus.Messaging", command);
            }

            return new StatusCodeResult(500);
        }


        private BusConfiguration GetBusConfig()
        {
            var busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName("Novanet.NetCoreNServiceBus.API");
            busConfiguration.UseSerialization<NServiceBus.JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(type =>
            {
                return type.Name.EndsWith("Command");
            });

            return busConfiguration;
        }
    }
}
