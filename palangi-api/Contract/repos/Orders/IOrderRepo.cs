using DomainModel.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.repos.Orders
{
    public interface IOrderRepo
    {
        Task<Order> Create(Order order);
    }
}
