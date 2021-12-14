using DomainModel.Orders;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace palangi_api.ControllerValidation
{
    public class PlaceOrderValidation : IControllerValidation<Order>
    {
        private readonly IEnumerable<IModelValidationItem<Order>> controllerValidationItems;
        private readonly IEnumerable<IExtraValidationItem> extraValidationItems;

        public string Key => "order";
        public PlaceOrderValidation(IEnumerable<IModelValidationItem<Order>> controllerValidationItems,
            IEnumerable<IExtraValidationItem> extraValidationItems)
        {
            this.controllerValidationItems = controllerValidationItems;
            this.extraValidationItems = extraValidationItems;
        }
        public async Task<List<Message>> Validate(Order order)
        {
            var result = new List<Message>();
            extraValidationItems.Where(vi => vi.Keys.Any(key => key == Key)).ToList().ForEach(vi =>
            {
                result.Add(vi.Validate().Result);
            });
            controllerValidationItems.Where(vi => vi.Key == Key).ToList().ForEach(vi => { result.Add(vi.Validate(order).Result); });
            return result;
        }
    }
}
