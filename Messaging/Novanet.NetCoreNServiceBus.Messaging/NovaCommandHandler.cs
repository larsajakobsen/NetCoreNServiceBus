
using System;
using System.Threading.Tasks;
using Novanet.NetCoreNServiceBus.Contracts;
using NServiceBus;
using NServiceBus.Logging;

namespace Novanet.NetCoreNServiceBus.Messaging
{

    public class NovaCommandHandler : IHandleMessages<NovaCommand>
    {
        public void Handle(NovaCommand message)
        {
            Console.WriteLine("I got a NovaCommand!");
        }
    }
}