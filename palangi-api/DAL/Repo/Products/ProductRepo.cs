using Contract.repos.Products;
using DAL.Context;
using DomainModel.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo.Products
{
    public class ProductRepo : IProductRepo
    {
        private readonly OrderContext context;

        public ProductRepo(OrderContext context)
        {
            this.context = context;
        }
        public async Task<Product> GetById(Guid productId)
        {
            Product product = await context.Products.Include(pr=>pr.ProductProductTypes).ThenInclude(ppt=>ppt.ProductType).FirstOrDefaultAsync(product=>product.ProductId==productId);
            return product;
        }
    }
}
