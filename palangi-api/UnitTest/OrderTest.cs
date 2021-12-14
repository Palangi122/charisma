using Contract.repos;
using Contract.repos.Posts;
using Contract.services.Orders;
using DomainModel.Orders;
using DomainModel.Posts;
using DomainModel.Pricing.Discounts;
using DomainModel.Products;
using FluentAssertions;
using Moq;
using Order_api.Services.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class OrderTest
    {
        private readonly IDefinPostType definPostType;
        private readonly Mock<IPostTypeRepo> postTypeRepo;


        private readonly Mock<IDiscountRepo> discountRepo;
        private readonly IDiscountCalc discountCalc;
        public OrderTest()
        {
            postTypeRepo = new Mock<IPostTypeRepo>();
            definPostType = new DefinPostType(postTypeRepo.Object);

            discountRepo = new Mock<IDiscountRepo>();
            discountCalc = new DiscountCalc(discountRepo.Object);
        }

        [Fact]
        public async Task DefinPostType_when_product_is_fragile_be_pishtaz()
        {
            Order order = new Order
            {
                OrderItems = new List<OrderItem> {
            new OrderItem{ Product=new DomainModel.Products.Product{ ProductProductTypes=new List<DomainModel.Products.ProductProductType>{
            new DomainModel.Products.ProductProductType{ ProductType=new DomainModel.Products.ProductType{ ProductTypeId = Guid.Parse("ad05d734-52f6-4caf-a6bf-66a9b30dcaa1"), Title = "معمولی" ,QueryTitle="fragile"} }
            } } },
            }
            };

            postTypeRepo.Setup(a => a.GetById(Guid.Parse("9af9ca7a-780e-4f3a-90a6-5d8edd739754"))).Returns(
                Task.FromResult(new PostType { PostTypeId = Guid.Parse("9af9ca7a-780e-4f3a-90a6-5d8edd739754"), Title = "سفارشی" })
                );


            await definPostType.Define(order);

            order.PostType.PostTypeId.Should().Be(Guid.Parse("9af9ca7a-780e-4f3a-90a6-5d8edd739754"));
        }
        [Fact]
        public async Task DefinPostType_when_product_is_not_fragile_be_general()
        {
            Order order = new Order
            {
                OrderItems = new List<OrderItem> {
            new OrderItem{ Product=new DomainModel.Products.Product{ ProductProductTypes=new List<DomainModel.Products.ProductProductType>{
            new DomainModel.Products.ProductProductType{ ProductType=new DomainModel.Products.ProductType{ ProductTypeId = Guid.Parse("ad05d734-52f6-4caf-a6bf-66a9b30dcaa1"), Title = "معمولی" ,QueryTitle="general"} }
            } } },
            }
            };

            postTypeRepo.Setup(a => a.GetById(Guid.Parse("890c7704-fbe2-4e7a-8227-76be42cb87d2"))).Returns(
                Task.FromResult(new PostType { PostTypeId = Guid.Parse("890c7704-fbe2-4e7a-8227-76be42cb87d2"), Title = "معمولی" })
                );


            await definPostType.Define(order);

            order.PostType.PostTypeId.Should().Be(Guid.Parse("890c7704-fbe2-4e7a-8227-76be42cb87d2"));
        }
        [Fact]
        public async Task DiscountCalc_when_discountNO_is_empty_order_discountAmount_be_zero()
        {
            Order order = new Order { DiscountNO = "" };
            discountRepo.Setup(a => a.GetByNO("")).Returns(Task.FromResult<Discount>(null));

            await discountCalc.Calc(order);

            order.DiscountAmount.Should().Be(0);

        }  
        [Fact]
        public async Task DiscountCalc_when_discountType_is_fix_order_discountAmount_be_discount_quantity()
        {
            var _discount = new Discount { DiscountId = Guid.Parse("c3020714-fcd9-41ee-b27e-db4e56fd0106"), Title = "ثابت", Quantity = 150, DiscountType = DiscountType.fix, DiscountNO = "dis_123" };
            Order order = new Order { DiscountNO = "dis_123" };
            discountRepo.Setup(a => a.GetByNO("dis_123")).Returns(Task.FromResult(_discount));

            await discountCalc.Calc(order);

            order.DiscountAmount.Should().Be(_discount.Quantity);

        }

        [Fact]
        public async Task DiscountCalc_when_discountType_is_percentage_order_discountAmount_be_ordertotalPercentage()
        {
            var _discount = new Discount { DiscountId = Guid.Parse("626eb365-4e87-4d32-98d3-2bc3e04d7520"), Title = "درصد", Quantity = 20, DiscountType = DiscountType.percentage, DiscountNO = "dis_321" };
            Order order = new Order { DiscountNO = "dis_321" ,Total=1000};
            discountRepo.Setup(a => a.GetByNO("dis_321")).Returns(Task.FromResult(_discount));

            await discountCalc.Calc(order);

            order.DiscountAmount.Should().Be(200);

        }

        [Fact]
        public async Task OrderPriceCalc_when_orderitem_has_withmarkup()
        {
            OrderItem oi_1 = new OrderItem { WithMarkup = true, Qty = 1, Product = new Product { ProductId = Guid.Parse("2c0e0a33-e6fd-4136-8725-089fbe8779e1"), Title = "کالای یک", Markup = 10.0, Price = 100 } };
            OrderItem oi_2 =new OrderItem { WithMarkup=false,Qty=1,Product= new Product { ProductId = Guid.Parse("fb49e3cd-79e6-4329-816d-91437a20d90c"), Title = "کالای دو", Markup = 0.0, Price = 120 } };
            Order order = new Order { OrderItems = new List<OrderItem> { oi_1, oi_2 } };

            OrderPriceCalc orderPriceCalc = new OrderPriceCalc();
            await orderPriceCalc.CalcPrice(order);

            order.Total.Should().Be(230);
        }
        [Fact]
        public async Task OrderPriceCalc_when_orderitem_has_not_withmarkup()
        {
            OrderItem oi_1 = new OrderItem { WithMarkup = false, Qty = 1, Product = new Product { ProductId = Guid.Parse("2c0e0a33-e6fd-4136-8725-089fbe8779e1"), Title = "کالای یک", Markup = 10.0, Price = 100 } };
            OrderItem oi_2 = new OrderItem { WithMarkup = false, Qty = 1, Product = new Product { ProductId = Guid.Parse("fb49e3cd-79e6-4329-816d-91437a20d90c"), Title = "کالای دو", Markup = 0.0, Price = 120 } };
            Order order = new Order { OrderItems = new List<OrderItem>{ oi_1, oi_2 } };

            OrderPriceCalc orderPriceCalc = new OrderPriceCalc();
            await orderPriceCalc.CalcPrice(order);

            order.Total.Should().Be(220);
        }
        [Fact]
        public async Task OrderPriceCalc_when_orderitem_has_qty()
        {
            OrderItem oi_1 = new OrderItem { WithMarkup = true, Qty = 3, Product = new Product { ProductId = Guid.Parse("2c0e0a33-e6fd-4136-8725-089fbe8779e1"), Title = "کالای یک", Markup = 10.0, Price = 100 } };
            OrderItem oi_2 = new OrderItem { WithMarkup = false, Qty = 2, Product = new Product { ProductId = Guid.Parse("fb49e3cd-79e6-4329-816d-91437a20d90c"), Title = "کالای دو", Markup = 0.0, Price = 120 } };
            Order order = new Order { OrderItems = new List<OrderItem> { oi_1, oi_2 } };

            OrderPriceCalc orderPriceCalc = new OrderPriceCalc();
            await orderPriceCalc.CalcPrice(order);

            order.Total.Should().Be(570);
        }
    }
}
