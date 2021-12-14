using Contract.repos;
using DAL.Context;
using DomainModel.Pricing.Discounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Discounts
{
    public class DiscountRepo: IDiscountRepo
    {
        private readonly OrderContext context;

        public DiscountRepo(OrderContext context)
        {
            this.context = context;
        }
        public async Task<Discount> GetByNO(string nO) {

            return await context.Discounts.FirstOrDefaultAsync(ds => ds.DiscountNO == nO);
        }
    }
}
