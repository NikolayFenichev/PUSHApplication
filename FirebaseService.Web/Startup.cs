using Firebase.BLL.Configuration;
using Firebase.BLL.Services.Interfaces;
using MessageQueueConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirebaseService.Web
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
            services.AddSingleton<IFirebaseService, Firebase.BLL.Services.FirebaseService>();
            services.AddSingleton<IFirebaseKey, FirebaseKey>(serviceProvider =>
            {
                var serverKey = Configuration["FirebaseKeys:ServerKey"];
                var senderId = Configuration["FirebaseKey:SenderId"];
                return new FirebaseKey(serverKey, senderId);
            });
            services.AddSingleton<IConsumerConfiguration, RabbitConsumerConfiguration>(serviceProvider =>
            {
                var host = Configuration["HostName:HostNameConsume"];
                var routingKeys = Configuration["RoutingKeys:FirebaseMessageQueue"];
                return new RabbitConsumerConfiguration(host, routingKeys);
            });

            services.Configure<KestrelServerOptions>(
            Configuration.GetSection("Kestrel"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
