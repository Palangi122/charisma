using DomainModel.Orders;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using palangi_api.Controllers;
using palangi_api.ControllerValidation;
using palangi_api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace palangi_api
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
            services.AddSingleton(Configuration.GetSection("sellTimeConfig").Get<SellTimeConfig>());
            services.AddTransient<IExtraValidationItem, TimeValidation>();
            services.AddTransient<IModelValidationItem<Order>, CustomerExist>();
            services.AddTransient<IControllerValidation<Order>, PlaceOrderValidation>();
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<UpdateCartableQueryModel>();
                //x.AddConsumer<UpdateDraftQueryModel>();
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

                    // cfg.ReceiveEndpoint("PlaceOrderTestQueue", e => { e.ConfigureConsumer<Order>(context); });
                    //cfg.ReceiveEndpoint("UpdateDraftQueryTestQueue", e => { e.ConfigureConsumer<UpdateDraftQueryModel>(context); });
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
