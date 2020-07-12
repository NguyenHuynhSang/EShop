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
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<ExchangeRateDongA> ExchangeRateDongAs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) //Một bảng có 2 khóa chính phải sử dụng fluent API
        {
            modelBuilder.Entity<ContentTag>()
                .HasKey(o => new { o.TagID, o.ContentID });
            // auto increment key



            modelBuilder.Entity<ProductVersion>().HasData(new ProductVersion { ID = 1, SKU = "Iphone test", Barcode = "COC", Price = 19000000, ProductID = 1, RemainingAmount = 100, Quantum = 100, Description = "Màu đỏ dl 250" },
                                               new ProductVersion { ID = 2, SKU = "Iphone test", Barcode = "COC", Price = 18000000, ProductID = 1, RemainingAmount = 100, Quantum = 100, Description = "Màu xanh dl 250" },
                                               new ProductVersion { ID = 3, SKU = "Iphone test", Barcode = "COC", Price = 16000000, ProductID = 2, RemainingAmount = 100, Quantum = 100, Description = "Màu xanh dl 250" });


            modelBuilder.Entity<ProductCatalog>().HasData(new ProductCatalog { ID = 1, ParentID = null, Name = "Điện thoại" },
                                                 new ProductCatalog { ID = 2, ParentID = null, Name = "Laptop" },
                                                 new ProductCatalog { ID = 3, ParentID = 1, Name = "Samsung" },
                                                 new ProductCatalog { ID = 4, ParentID = 1, Name = "Apple" },
                                                 new ProductCatalog { ID = 5, ParentID = 2, Name = "Macbook" });



            modelBuilder.Entity<Product>().HasData(new Product { ID = 1, CatalogID = 1, Name = "Iphone test", ApplyPromotion = true, OriginalPrice = 16000000, Content = "This is an iphone", Deliver = true, Description = "no discrip" },
                                                 new Product { ID = 2, CatalogID = 1, Name = "samsung galaxy test", ApplyPromotion = true, OriginalPrice = 14000000, Content = "This is a samsung", Deliver = true, Description = "no discrip" });

            modelBuilder.Entity<EShop.Server.Models.Attribute>().HasData(new ProductAttribute { ID = 1, Name = "Màu sắc" },
                                                 new ProductAttribute { ID = 2, Name = "Dung lượng" });

            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue { ID = 1, AttributeID = 1, Name = "Đỏ" },
                                                new AttributeValue { ID = 2, AttributeID = 1, Name = "Xanh" },
                                               new AttributeValue { ID = 3, AttributeID = 1, Name = "Tím" },
                                                new AttributeValue { ID = 4, AttributeID = 2, Name = "16gb" },
                                                new AttributeValue { ID = 5, AttributeID = 2, Name = "32gb" },
                                                new AttributeValue { ID = 6, AttributeID = 2, Name = "64gb" });








        }



    }
}
