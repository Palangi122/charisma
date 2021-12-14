using Contract.services.Orders;
using DAL.Context;
using DAL.StartAndDI;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order_api.Consumers;
using Order_api.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<UpdateCartableQueryModel>();
                x.AddConsumer<PlaceOrderConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    //cfg.Send<SubmitOrder>(so => { so.UseCorrelationId(context => context.OrderId); });
                    //cfg.ConfigureEndpoints()
                    // cfg.ConfigureEndpoints(context);
                    //cfg.ReceiveEndpoint("ErjaLetterLetterQueue", e => { e.ConfigureConsumer<ErjaLetterConsumer>(context); });

                    //cfg.ReceiveEndpoint("place-order", e =>
                    //{
                    //    //e.Consumer<PlaceOrderConsumer>();
                    //    e.Consumer<PlaceOrderConsumer>();
                    //});
                    cfg.ReceiveEndpoint("place-order", e => { e.ConfigureConsumer<PlaceOrderConsumer>(context); });

                    //cfg.ReceiveEndpoint("UpdateDraftQueryTestQueue", e => { e.ConfigureConsumer<UpdateDraftQueryModel>(context); });
                });
            });

            //dal config
            services.AddDalEF(Configuration.GetConnectionString("orderConnection"));
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IDefinPostType, DefinPostType>();
            services.AddTransient<IOrderPriceCalc, OrderPriceCalc>();
            services.AddTransient<IDiscountCalc, DiscountCalc>();

            services.AddControllers();
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            SeedDb.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
