using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Sales.Messages.Commands;
using Sales.Messages.Events;
using System.Collections.Generic;

namespace Sales.Order.OrderCreated
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder:\nUserId: {message.UserId}\nOrders:\n {String.Join("\n", message.ProductIds)}\n{message.ShippingTypeId}, {message.TimeStamp}");

            var orderId = Database.SaveOrder(message.ProductIds, message.UserId, message.ShippingTypeId);

            var orderCreatedEvent = new Sales.Messages.Events.OrderCreated
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTime.Now,
                Amount = CalculateCostOf(message.ProductIds)
            };
            return context.Publish(orderCreatedEvent);
        }

        private double CalculateCostOf(IEnumerable<string> productIds)
        {
            // Fake database lookup
            return 1000.00;
        }

    }

    public class Database
    {
        private static int Id = 0;

        public static string SaveOrder(IEnumerable<string> productIds, string userId, string shippingTypeId)
        {
            var nextOrderId = Id++;
            return nextOrderId.ToString();
        }
    }

}
