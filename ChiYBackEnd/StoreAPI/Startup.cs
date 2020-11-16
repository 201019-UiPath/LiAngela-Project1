using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StoreDB;
using StoreDB.Repos;
using StoreLib;

namespace StoreAPI
{
    public class Startup
    {
        readonly string MyAllowedOrigin = "myAllowedOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(name: MyAllowedOrigin,
                    builder => {
                        builder.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services.AddControllers().AddXmlSerializerFormatters();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<StoreContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ChiYDB")));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepo, DBRepo>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILocationRepo, DBRepo>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepo, DBRepo>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepo, DBRepo>();
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

            app.UseCors(MyAllowedOrigin);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
