using Contract.repos;
using Contract.services.Orders;
using DomainModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_api.Services.Orders
{
    public class DiscountCalc : IDiscountCalc
    {
        private readonly IDiscountRepo discountRepo;

        public DiscountCalc(IDiscountRepo discountRepo)
        {
            this.discountRepo = discountRepo;
        }
        public async Task Calc(Order order)
        {
            var discount = await this.discountRepo.GetByNO(order.DiscountNO);
            if(discount != null)
            {
                if (discount.DiscountType == DomainModel.Pricing.Discounts.DiscountType.fix)
                {
                    order.DiscountAmount = discount.Quantity;// میبایست چک شود از مبلغ فاکتور بیشتر نباشد
                }
                else
                {
                    order.DiscountAmount = discount.Quantity * order.Total / 100;
                }
            }
        }
    }
}
