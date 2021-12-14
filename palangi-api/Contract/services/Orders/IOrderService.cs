using DomainModel.Orders;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.services.Orders
{
   public  interface IOrderService
    {
        Task<Response<Order>> Create(Order order);

    }
}
