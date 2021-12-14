using Contract.repos;
using Contract.repos.Orders;
using Contract.repos.Posts;
using Contract.repos.Products;
using DAL.Context;
using DAL.Repo.Discounts;
using DAL.Repo.Orders;
using DAL.Repo.Posts;
using DAL.Repo.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.StartAndDI
{
    public static class DalDI
    {
        public static void AddDalEF(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(connectionString));
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<IPostTypeRepo, PostTypeRepo>();
            services.AddTransient<IDiscountRepo, DiscountRepo>();
         
        }
    }
}
