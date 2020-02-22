using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Billing.Payments.PaymentAccepted
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Billing - Payment Accepted";

            var endpointConfiguration = new EndpointConfiguration("Bus");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to Exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
