using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Pricing.Discounts
{
    public class Discount
    {
        public Guid DiscountId { get; set; }
        public string Title { get; set; }
        public double Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public string DiscountNO { get; set; }

    }
    public enum DiscountType
    {
        fix=1,
        percentage = 2
    }
}
