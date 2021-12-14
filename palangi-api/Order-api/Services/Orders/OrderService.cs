using Contract.repos.Orders;
using Contract.repos.Products;
using Contract.services.Orders;
using DomainModel.Orders;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_api.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo orderRepo;
        private readonly IProductRepo productRepo;
        private readonly IDefinPostType definPostType;
        private readonly IOrderPriceCalc orderPriceCalc;
        private readonly IDiscountCalc discountCalc;

        public OrderService(IOrderRepo orderRepo,
            IProductRepo productRepo,
            IDefinPostType definPostType,
            IOrderPriceCalc orderPriceCalc,
            IDiscountCalc discountCalc)
        {
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.definPostType = definPostType;
            this.orderPriceCalc = orderPriceCalc;
            this.discountCalc = discountCalc;
        }
        public async Task<Response<Order>> Create(Order order)
        {
            Response<Order> response = new Response<Order> { ActionResult = ActResult.Successful };
            try
            {
                await SetProductInfo(order);
                await this.orderPriceCalc.CalcPrice(order);
                await this.definPostType.Define(order);
                await this.discountCalc.Calc(order);
                order.TotalOrder = order.Total - order.DiscountAmount;
                response.Result = await this.orderRepo.Create(order);
            }
            catch (Exception ex)
            {

                response.ActionResult = ActResult.Unsuccessful;
                //log exception

            }
            return response;
        }
        public async Task SetProductInfo(Order order)
        {
            foreach (var orderItem in order.OrderItems)
                orderItem.Product = await this.productRepo.GetById(orderItem.ProductId);
        }

    }
}
