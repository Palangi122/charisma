using Contract.repos.Orders;
using DAL.Context;
using DomainModel.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Orders
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderContext context;

        public OrderRepo(OrderContext context)
        {
            this.context = context;
        }
        public async Task<Order> Create(Order order)
        {
            try
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
            return order;
        }
    }
}
