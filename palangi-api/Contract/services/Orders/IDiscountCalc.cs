using DomainModel.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.services.Orders
{
    public interface IDiscountCalc
    {
        Task Calc(Order order);
    }
}
