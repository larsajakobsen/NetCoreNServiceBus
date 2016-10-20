
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Novanet.NetCoreNServiceBus.Handler
{
    public class UnicastBusConfigOverride : IProvideConfiguration<UnicastBusConfig>
    {
        // public UnicastBusConfigOverride (MessageEndpointMappings mappings)
        // {

        // }

        public UnicastBusConfig GetConfiguration()
        {
            var mappings = new MessageEndpointMappingCollection();

            //TODO: Use values from appsettings. Difficult because of NServiceBus dependency injection. Possible solution, create custom NServiceBus container (implementing IContainer) to use .NET Core container.
            mappings.Add(new MessageEndpointMapping { Messages = "Novanet.NetCoreNServiceBus.Contracts", Endpoint = "Novanet.NetCoreNServiceBus.Messaging@localhost" });

            return new UnicastBusConfig()
            {
                MessageEndpointMappings = mappings
            };
        }
    }

    public class ConfigErrorQueue : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "novanet.netcoreservicebus.messaging.error"
            };
        }
    }
}