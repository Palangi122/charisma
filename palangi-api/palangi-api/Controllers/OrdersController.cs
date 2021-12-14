using DomainModel.Orders;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using palangi_api.ControllerValidation;
using palangi_api.Utilities.Helpers;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace palangi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IControllerValidation<Order> orderValidation;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public OrdersController(
            IControllerValidation<Order> orderValidation,
            ISendEndpointProvider sendEndpointProvider)
        {
            this.orderValidation = orderValidation;
            this.sendEndpointProvider = sendEndpointProvider;
        }
        [HttpPost]
        public async Task<Shared.Response> PlaceOrder([FromBody]Order order)
        {
            var response = ResponseFactory.Create<Order>(ActResult.Successful).Result;

            order.OrderId = Guid.NewGuid();

            //validation
            var validation = this.orderValidation.Validate(order).Result;

            response.ActionResult = validation.All(v => v.ActResult == ActResult.Successful) ? ActResult.Successful : ActResult.Unsuccessful;
            if (response.ActionResult == ActResult.Successful)
            {
                //set server variable
                order.CreateDate = DateTime.Now;

                // send order ot order-api
                var endp = this.sendEndpointProvider.GetSendEndpoint(new Uri("queue:place-order")).Result;
                endp.Send(order).Wait();
            }
            else
            {
                response.Messages = validation;
                //if(vx.Count)
            }
            return response;
        }
    }
}
