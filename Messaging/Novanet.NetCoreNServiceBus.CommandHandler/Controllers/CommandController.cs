using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using NServiceBus;

namespace Novanet.NetCoreNServiceBus.CommandHandler.Controllers
{

    [RoutePrefix("api/command")]
    public class CommandController : ApiController
    {
        private readonly IBus _bus;

        public CommandController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [Route("dispatch")]
        public async Task Dispatch(HttpRequestMessage request)
        {
            var busConfiguration = GetBusConfig();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                var jsonString = await request.Content.ReadAsStringAsync();

                var obj =
                    JsonConvert.DeserializeObject(jsonString,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });

                bus.Send(obj);
            }
        }

        private BusConfiguration GetBusConfig()
        {
            var busConfiguration = new BusConfiguration();

            // The endpoint name will be used to determine queue names and serves
            // as the address, or identity, of the endpoint
            busConfiguration.EndpointName("Novanet.NetCoreNServiceBus.Messaging");

            // Use JSON to serialize and deserialize messages (which are just
            // plain classes) to and from message queues
            busConfiguration.UseSerialization<NServiceBus.JsonSerializer>();

            // Ask NServiceBus to automatically create message queues
            busConfiguration.EnableInstallers();

            // Store information in memory for this example, rather than in
            // a database. In this sample, only subscription information is stored
            busConfiguration.UsePersistence<InMemoryPersistence>();

            return busConfiguration;
        }
    }
}