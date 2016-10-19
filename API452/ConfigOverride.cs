
namespace Novanet.NetCoreNServiceBus.Handler
{
    class ConfigOverride : IProvideConfiguration<UnicastBusConfig>
    {
        public UnicastBusConfig GetConfiguration()
        {
            return new UnicastBusConfig
            {
                MessageEndpointMappings = new MessageEndpointMappingCollection
                {
                    new MessageEndpointMapping { Messages="MyMessages", Endpoint="orderserviceinputqueue" }
                }
            };
        }
    }
}