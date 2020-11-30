using EShop.Server.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EShop.Server.Extension;

namespace EShop.Server.Data
{
    using ProductAttribute = Server.Models.Attribute;

    public class EShopDbContext : DbContext
    {
        //private static DbContextOptions GetOptions(string connectionString)
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        //}
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        {

        }

        public static EShopDbContext Create(DbContextOptions<EShopDbContext> options)
        {
            return new EShopDbContext(options);
        }

        /// <summary>
        /// Add db here
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ContentCategory> ContentCategories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentTag> contentTags { get; set; }

        public DbSet<ProductCatalog> ProductCatalogs { get; set; }

        public DbSet<EShop.Server.Models.Attribute> Attributes { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<ProductVersion> ProductVersions { get; set; }

        public DbSet<ProductVersionImage> ProductVersionImages { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductVersionAttribute> ProductVersionAttributes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<ExchangeRateDongA> ExchangeRateDongAs { get; set; }

        public DbSet<SeedLog> SeedLogs { get; set; }


        public DbSet<Slide> Slides { get; set; }
        public DbSet<SlideGroup> SlideGroups { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //Một bảng có 2 khóa chính phải sử dụng fluent API
        {

            //use Fluent Api to customize a joining table name and column names
            // ref: https://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx

            //- EF core does not support many-to-many relationships directly.
            //modelBuilder.Entity<AttributeValue>()
            //  .HasMany<ProductVersion>(s => s.ProductVersions)
            //  w .(c => c.ProductVersions)
            //  .Map(cs =>
            //  {
            //      cs.MapLeftKey("StudentRefId");
            //      cs.MapRightKey("CourseRefId");
            //      cs.ToTable("ProductVersionAttribute");
            //  });

            //update
            modelBuilder.Entity<ProductVersionAttribute>().HasKey(sc => new { sc.AttributeValueID, sc.ProductVersionID });

            modelBuilder.Entity<ContentTag>()
                .HasKey(o => new { o.TagID, o.ContentID });
            // auto increment 
            SeedProductAttribute(modelBuilder);
            SeedAttributeValue(modelBuilder);
            SeedProductCatalog(modelBuilder);
           // SeedProduct(modelBuilder);
        }


        ///Seed product attribute from file
        ///
        private void SeedProductAttribute(ModelBuilder modelBuilder)
        {

            var dataJson = File.ReadAllText("Data/product.attribute.data.json");
            var attrobutes = JsonConvert.DeserializeObject<List<Models.Attribute>>(dataJson);
            modelBuilder.Entity<Models.Attribute>().HasData(attrobutes);
        }


        ///Seed Attribute value from file
        ///
        private void SeedAttributeValue(ModelBuilder modelBuilder)
        {

            var dataJson = File.ReadAllText("Data/attribute.value.data.json");
            var data = JsonConvert.DeserializeObject<List<Models.AttributeValue>>(dataJson);
            modelBuilder.Entity<Models.AttributeValue>().HasData(data);
        }
        private void SeedProductCatalog(ModelBuilder modelBuilder)
        {


            var dataJson = File.ReadAllText("Data/product.catalog.data.json");
            var data = JsonConvert.DeserializeObject<List<Models.ProductCatalog>>(dataJson);
            modelBuilder.Entity<ProductCatalog>().HasData(data);
        }




        //private void SeedProduct(ModelBuilder modelBuilder)
        //{
        //    var dataJson = File.ReadAllText("Data/product.data.json");
        //    var data = JsonConvert.DeserializeObject<List<Models.Product>>(dataJson);
        //    modelBuilder.Entity<Product>().HasData(data);


        //}



    }
}
