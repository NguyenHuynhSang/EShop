using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Server.Data;
using EShop.Server.Extension;
using EShop.Server.Repository;
using EShop.Server.SchedulerTask;
using EShop.Server.Service;
using EShop.Server.global;
using EShop.Server.Service.HostService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;
using EShop.Server.Helper;
using System.Configuration;
using StackExchange.Redis;
using EShop.Server.Data.Repository;
using EShop.Server.Entities;
using EShop.Server.Client.Service;
using EShop.Server.Server.Service;
using GHNApi;

namespace EShop.Server
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
            services.AddDbContext<EShopDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            services.AddCors(op =>
           {
               op.AddPolicy(name: global.global.ApiCorsPolicy,
                   builder =>
                   {
                       builder.WithOrigins(global.global.Origins)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin()
                                           ;
                   });

           }
            );

            services.Configure<CloudinarySetting>(Configuration.GetSection("CloudinarySetting"));
            services.Configure<SmtpSetting>(Configuration.GetSection("SmtpSetting"));


            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<Seed>();

            services.AddScoped<ISlideRepository, SlideRepository>();
            services.AddScoped<ISlideClientService, SlideClientService>();


            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductClientService, ProductClientService>();
            services.AddScoped<IProductVersionRepository, ProductVersionRepository>();
            services.AddScoped<IProductCatalogClientService, ProductCatalogClientService>();

            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICatalogService, CatalogService>();
     
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IAttributeService, AttributeService>();

            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            services.AddScoped<IAttributeValueService, AttributeValueService>();

            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
          


            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();


            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderClientService, OrderClientService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuClientService, MenuClientService>();

            services.AddScoped<IGiaoHangNhanhService, GiaoHangNhanhService>();
            






            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((options) =>
            {
                Console.WriteLine(Configuration.GetSection("AppSettings:Token").Value);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.DescribeAllEnumsAsStrings();
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });

                //swagger.DocumentFilter<CustomModelDocumentForSwagger<ProductFilterModel>>();
            });

            //add scheduled task
            services.AddSingleton<IScheduledTask, ExchangeRateTask>();
            services.AddScheduler((sender, args) =>
            {
                Console.Write(args.Exception.Message);
                args.SetObserved();
            });
            services.AddRouting(options => options.LowercaseUrls = true);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "Doc")),
                RequestPath = "/Doc"
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
            app.UseCors(global.global.ApiCorsPolicy);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");

                //  c.RoutePrefix = string.Empty;
            });
        }
    }
}
