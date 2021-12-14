using Contract.services.Orders;
using DomainModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_api.Services.Orders
{
    public class OrderPriceCalc : IOrderPriceCalc
    {
        public async Task CalcPrice(Order order)
        {
            order.Total=order.OrderItems.Sum(oi => (oi.WithMarkup ? (oi.Product.Markup + oi.Product.Price) : oi.Product.Price) * oi.Qty);
        }
    }
}
