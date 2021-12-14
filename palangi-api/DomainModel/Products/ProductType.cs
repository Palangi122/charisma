using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Products
{
    public class ProductType
    {
        public Guid ProductTypeId { get; set; }
        public string Title { get; set; }
        public string QueryTitle { get; set; }
        public List<ProductProductType> ProductProductTypes { get; set; }

    }
}
