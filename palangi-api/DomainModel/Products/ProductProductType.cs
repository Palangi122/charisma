using System;

namespace DomainModel.Products
{
    public class ProductProductType {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public ProductType ProductType { get; set; }
        public Guid ProductTypeId { get; set; }

    }
}
