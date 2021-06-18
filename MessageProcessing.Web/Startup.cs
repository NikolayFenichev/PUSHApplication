using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageProcessing.BLL.Services;
using MessageProcessing.BLL.Services.Interfaces;
using MessageQueueConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PUSHApplication.DAL;
using PUSHApplication.DAL.Repositories;
using PUSHApplication.DAL.Repositories.Interfaces;

namespace MessageProcessing.Web
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
            services.AddSingleton<IMessageProcessingService, MessageProcessingService>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConsumerConfiguration, RabbitConsumerConfiguration>(serviceProvider =>
            {
                var host = Configuration["HostName:HostNameConsume"];
                var routingKeys = Configuration["RoutingKeys:MessageQueue"];
                return new RabbitConsumerConfiguration(host, routingKeys);
            });
            services.AddSingleton<IProducerConfiguration, RabbitProducerConfiguration>(serviceProvider =>
            {
                var host = Configuration["HostName:HostNameProduce"];
                var routingKeys = Configuration["RoutingKeys:FirebaseMessageQueue"];
                return new RabbitProducerConfiguration(host, routingKeys);
            });
            services.AddDbContext<PUSHApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")), ServiceLifetime.Singleton);
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
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
