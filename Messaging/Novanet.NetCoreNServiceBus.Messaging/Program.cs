using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Novanet.NetCoreNServiceBus.Messaging
{
    public class Program
    {
        public static void Main()
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Novanet.NetCoreNServiceBus.Messaging");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(type =>
            {
                return type.Name.EndsWith("Command");
            });

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
