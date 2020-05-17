using EShop.Data;
using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

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
            services.AddMvc(option => option.EnableEndpointRouting = false);

            //                .AddControllersAsServices();      // <---- Super important

            services.AddControllers();

            services.AddDbContext<EShopDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EShopDbContext")));

            //   services.AddScoped<EShopDbContext, EShopDbContext>();

            //inject tùm lum khúc này, cần tìm hiểu thêm

            services.AddScoped<IDbFactory, DbFactory>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();

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



            services.AddScoped<ICatalogRepository,CatalogRepository>();
            services.AddScoped<ICatalogService,CatalogService>();

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
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                );
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                //  c.RoutePrefix = string.Empty;
            });

            /// Load các cấu trúc tự tạo vào project
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
                RequestPath = "/Assets"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "app")),
                RequestPath = "/app"
            });
        }
    }
}