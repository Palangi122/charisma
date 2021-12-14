using DomainModel.Customers;
using DomainModel.Others;
using DomainModel.Posts;
using DomainModel.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Orders
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public PostType PostType { get; set; }
        public Guid? PostTypeId { get; set; }
        public string DiscountNO { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public double TotalMarkup { get; set; }
        public double TotalOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public Address Address { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class Order
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public PostType PostType { get; set; }
        public Guid? PostTypeId { get; set; }
        public string DiscountNO { get; set; }
        public double Total { get; set; }//قیمت بدون تخفیف
        //public double Discount { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public string Address { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public bool WithMarkup { get; set; }
        public double Qty { get; set; }
    }
}
