using Contract.repos.Posts;
using Contract.services.Orders;
using DomainModel.Orders;
using DomainModel.Posts;
using DomainModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_api.Services.Orders
{
    public class DefinPostType : IDefinPostType
    {
        private readonly IPostTypeRepo postTypeRepo;

        public DefinPostType(IPostTypeRepo postTypeRepo)
        {
            this.postTypeRepo = postTypeRepo;
        }
        public async Task Define(Order order)
        {
            var containFragile = order.OrderItems.Where(oi => oi.Product.ProductProductTypes.Any(pr => pr.ProductType.QueryTitle == "fragile")).ToList().Count>0;
            if (containFragile) order.PostType = await this.postTypeRepo.GetById(Guid.Parse("9af9ca7a-780e-4f3a-90a6-5d8edd739754"));//کد پست پیشتاز
            else order.PostType = await this.postTypeRepo.GetById(Guid.Parse("890c7704-fbe2-4e7a-8227-76be42cb87d2"));//کد پست معمولی
        }
    }
}
