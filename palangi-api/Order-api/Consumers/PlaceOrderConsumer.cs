using Contract.services.Orders;
using DomainModel.Orders;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_api.Consumers
{
    public class PlaceOrderConsumer : IConsumer<Order>
    {
        private readonly IOrderService orderService;

        public PlaceOrderConsumer(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public async Task Consume(ConsumeContext<Order> context)
        {
            
            await orderService.Create(context.Message);
        }
    }
}
