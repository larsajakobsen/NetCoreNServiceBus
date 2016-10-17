
using System;
using Novanet.NetCoreNServiceBus.Contracts;
using NServiceBus;

namespace Novanet.NetCoreNServiceBus.Messaging
{

    public class NovaCommandHandler : IHandleMessages<NovaCommand>
    {
        public void Handle(NovaCommand message)
        {
            Console.WriteLine($"I got a NovaCommand! {message.Id} {message.Name}");
        }
    }
}