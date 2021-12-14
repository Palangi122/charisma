using DomainModel.Pricing.Discounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.repos
{
    public interface IDiscountRepo
    {
        Task<Discount> GetByNO(string nO);
    }
}
