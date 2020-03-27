using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using EShop.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace EShop.WebApp  
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
            services.AddMvc();

            //                .AddControllersAsServices();      // <---- Super important
            
            services.AddControllers();


            services.AddDbContext<EShopDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EShopDbContext")));

            //   services.AddScoped<EShopDbContext, EShopDbContext>();

            //inject tùm lum khúc này, cần tìm hiểu thêm

            services.AddScoped<IDbFactory,DbFactory>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService,ProductService>();

            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<INewsService, NewsService>();




            services.AddCors(x => x.AddPolicy("EnableCORS",
                builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build()));
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors("EnableCORS");
           // app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });


          
        }
    }
}
