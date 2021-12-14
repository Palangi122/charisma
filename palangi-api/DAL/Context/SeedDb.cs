using DomainModel.Customers;
using DomainModel.Posts;
using DomainModel.Pricing.Discounts;
using DomainModel.Products;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Context
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<OrderContext>();

            if (context.Customers.ToList().Count > 0) return;
            Customer customer_1 = new Customer { CustomerId = Guid.Parse("0da1513c-2608-45df-8699-de2089bd5116"), FirstName = "mahmoud", LastName = "palangi", Addresses = "zirab" };
            Customer customer_2 = new Customer { CustomerId = Guid.Parse("ef114565-4138-4e1a-b128-408514369c60"), FirstName = "parmida", LastName = "palangi", Addresses = "tehran" };
            
            context.Customers.Add(customer_1);
            context.Customers.Add(customer_2);

            PostType postType_1 = new PostType {PostTypeId=Guid.Parse("890c7704-fbe2-4e7a-8227-76be42cb87d2"),Title="معمولی" };
            PostType postType_2 = new PostType {PostTypeId=Guid.Parse("9af9ca7a-780e-4f3a-90a6-5d8edd739754"),Title="سفارشی" };

            context.postTypes.Add(postType_1);
            context.postTypes.Add(postType_2);


            ProductType productType_1 = new ProductType { ProductTypeId = Guid.Parse("ad05d734-52f6-4caf-a6bf-66a9b30dcaa1"), Title = "معمولی" ,QueryTitle="general"};
            ProductType productType_2 = new ProductType { ProductTypeId = Guid.Parse("2dac633a-2f7f-4206-b14f-4684930c2ffa"), Title = "شکستنی" ,QueryTitle= "fragile" };
            context.ProductTypes.Add(productType_1);
            context.ProductTypes.Add(productType_2);

            Product product_1 = new Product { ProductId = Guid.Parse("2c0e0a33-e6fd-4136-8725-089fbe8779e1"), Title = "کالای یک" ,Markup=0.0,Price=100};
            Product product_2 = new Product { ProductId = Guid.Parse("fb49e3cd-79e6-4329-816d-91437a20d90c"), Title = "کالای دو" ,Markup=0.0,Price=120};
            Product product_3 = new Product { ProductId = Guid.Parse("1b61b42d-c2d7-4b98-b674-52fa12837554"), Title = "کالای سه" ,Markup=0.0,Price=110};
            Product product_4 = new Product { ProductId = Guid.Parse("6d85656d-4765-4a90-a72b-f6a03e294e6a"), Title = "کالای چهار" ,Markup=0.0,Price=210};
            context.Products.Add(product_1);
            context.Products.Add(product_2);
            context.Products.Add(product_3);
            context.Products.Add(product_4);


            ProductProductType ppt_1 = new ProductProductType { ProductId = product_1.ProductId, ProductTypeId = productType_1.ProductTypeId };
            ProductProductType ppt_2 = new ProductProductType { ProductId = product_2.ProductId, ProductTypeId = productType_2.ProductTypeId };
            ProductProductType ppt_3 = new ProductProductType { ProductId = product_3.ProductId, ProductTypeId = productType_1.ProductTypeId };
            ProductProductType ppt_4_1 = new ProductProductType { ProductId = product_4.ProductId, ProductTypeId = productType_1.ProductTypeId };
            ProductProductType ppt_4_2 = new ProductProductType { ProductId = product_4.ProductId, ProductTypeId = productType_2.ProductTypeId };
            context.ProductProductTypes.Add(ppt_1);
            context.ProductProductTypes.Add(ppt_2);
            context.ProductProductTypes.Add(ppt_3);
            context.ProductProductTypes.Add(ppt_4_1);
            context.ProductProductTypes.Add(ppt_4_2);


            Discount discount_1 = new Discount { DiscountId = Guid.Parse("c3020714-fcd9-41ee-b27e-db4e56fd0106"), Title = "ثابت", Quantity = 150,DiscountType=DiscountType.fix,DiscountNO="dis_123" };
            Discount discount_2 = new Discount { DiscountId = Guid.Parse("626eb365-4e87-4d32-98d3-2bc3e04d7520"), Title = "درصد", Quantity = 20 ,DiscountType=DiscountType.percentage,DiscountNO="dis_321"};
            context.Discounts.Add(discount_1);
            context.Discounts.Add(discount_2);

            
            context.SaveChanges();
        }
    }
}
