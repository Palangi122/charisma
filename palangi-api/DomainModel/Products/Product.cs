using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double Markup { get; set; }
        public List<ProductProductType> ProductProductTypes { get; set; }
    }
}
