using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Sales.Messages.Commands;

namespace DDDesign.Web.Controllers
{
    public class OrdersController : Controller
    {
        IMessageSession messageSession;
        public OrdersController(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Place(string userId, string productIds, string shippingTypeId)
        {
            var realProductIds = productIds.Split(',');
            var placeOrderCommand = new PlaceOrder
            {
                UserId = userId,
                ProductIds = realProductIds,
                ShippingTypeId = shippingTypeId,
                TimeStamp = DateTime.Now
            };
            await messageSession.Send(placeOrderCommand).
                ConfigureAwait(false);

            return "Your order has been placed and you will receive an email confirmation shortly";
            
        }
    }
}