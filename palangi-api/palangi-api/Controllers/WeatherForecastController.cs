using DomainModel.Orders;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using palangi_api.ControllerValidation;
using palangi_api.Dto;
using palangi_api.Utilities.Helpers;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace palangi_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISendEndpointProvider sendEndpointProvider;
        private readonly SellTimeConfig sellTimeConfig;
        private readonly IControllerValidation<Order> orderValidation;
        private readonly MyConfiguration myConfiguration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ISendEndpointProvider sendEndpointProvider,
            SellTimeConfig sellTimeConfig,
            IControllerValidation<Order> orderValidation)
        {
            _logger = logger;
            this.sendEndpointProvider = sendEndpointProvider;
            this.sellTimeConfig = sellTimeConfig;
            this.orderValidation = orderValidation;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Order order = new Order
            {
                Customer = new DomainModel.Customers.Customer { CustomerId = Guid.NewGuid() }
            };
            var vx = this.orderValidation.Validate(order).Result;
            var response=  ResponseFactory.Create<Order>(ActResult.Successful).Result;
            response.ActionResult = vx.All(v => v.ActResult == ActResult.Successful) ? ActResult.Successful : ActResult.Unsuccessful;
            if (response.ActionResult == ActResult.Unsuccessful)
            {
                response.Messages = vx;
                //if(vx.Count)
            }
            //var c = DateTime
            var endp = this.sendEndpointProvider.GetSendEndpoint(new Uri("queue:order-service")).Result;
            endp.Send(new SubmitOrder { OrderId = Guid.NewGuid() }).Wait();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
    public class SubmitOrder
    {
        public Guid OrderId { get; set; }
    }
    public class MyConfiguration
    {
        public bool MyProperty { get; set; }
    }
}
