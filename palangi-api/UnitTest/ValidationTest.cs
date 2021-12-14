using DomainModel.Customers;
using DomainModel.Orders;
using palangi_api.ControllerValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace UnitTest
{
    public class ValidationTest
    {
        [Fact]
        public void CustomerExist_when_customer_not_exist_be_CustomerNotBeNull()
        {
            Order order = new Order { OrderId = Guid.NewGuid(), Customer = null };

            var result = new CustomerExist().Validate(order).Result;

            result.ActResult.Should().Be(Shared.ActResult.CustomerNotBeNull);
        }
        [Fact]
        public void CustomerExist_when_customer_exist_be_()
        {

            Order order = new Order { OrderId = Guid.NewGuid(), Customer = new Customer() };

            var result = new CustomerExist().Validate(order).Result;

            result.ActResult.Should().Be(Shared.ActResult.Successful);
        }

    }
}
