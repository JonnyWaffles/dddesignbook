using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Events;


namespace Billing.Payments.PaymentAccepted
{
    class OrderCreatedHandler: IHandleMessages<OrderCreated>
    {
        static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(OrderCreated message, IMessageHandlerContext context)
        {
            log.Info($"Received order created event:\nOrderId: {message.OrderId}");

            return Task.CompletedTask;
        }
    }
}
