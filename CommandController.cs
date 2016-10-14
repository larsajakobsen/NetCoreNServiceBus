using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using NServiceBus;

namespace IfInsurance.Waypoint.Command.Dispatcher.Controllers
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
            var jsonString = await request.Content.ReadAsStringAsync();
            
            var obj =
                JsonConvert.DeserializeObject(jsonString,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

            _bus.Send(obj);
        }
    }
}
