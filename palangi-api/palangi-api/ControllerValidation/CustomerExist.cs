using DomainModel.Orders;
using Shared;
using System.Threading.Tasks;

namespace palangi_api.ControllerValidation
{
    public class CustomerExist : IModelValidationItem<Order>
    {
        public string Key => "order";

        public async Task<Message> Validate(Order order)
        {
            if (order.Customer is null)
                return new Message { ActResult = ActResult.CustomerNotBeNull, Body = "هر سفارش می بایست یک مشتری داشته باشد." };

            return new Message { ActResult = ActResult.Successful };
        }
    }public class CustomerExist1 : IModelValidationItem<Order>
    {
        public string Key => "order";

        public async Task<Message> Validate(Order order)
        {
            if (order.Customer is null)
                return new Message { ActResult = ActResult.CustomerNotBeNull, Body = "هر سفارش می بایست یک مشتری داشته باشد." };

            return new Message { ActResult = ActResult.Successful };
        }
    }
}
