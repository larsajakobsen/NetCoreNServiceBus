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
        public CommandController()
        {
        }

        [HttpPost]
        [Route("dispatch")]
        public async Task Dispatch(HttpRequestMessage request)
        {
            try
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

                    bus.Send("Novanet.NetCoreNServiceBus.Messaging", obj);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private BusConfiguration GetBusConfig()
        {
            var busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName("Novanet.NetCoreNServiceBus.CommandHandler");
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