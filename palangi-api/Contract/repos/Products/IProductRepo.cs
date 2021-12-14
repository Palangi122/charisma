using DomainModel.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.repos.Products
{
  public  interface IProductRepo
    {
        Task<Product> GetById(Guid productId);
    }
}
