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
            // auto increment key




            modelBuilder.Entity<ProductCatalog>().HasData(new ProductCatalog { ID = 1, ParentID = null, Name = "Áo" },
                                                 new ProductCatalog { ID = 2, ParentID = null, Name = "Quần" },
                                                 new ProductCatalog { ID = 3, ParentID = 1, Name = "Áo khoác" },
                                                 new ProductCatalog { ID = 4, ParentID = 1, Name = "Áo thun" },
                                                 new ProductCatalog { ID = 5, ParentID = 1, Name = "Áo sơ mi" });




            modelBuilder.Entity<EShop.Server.Models.Attribute>().HasData(new ProductAttribute { ID = 1, Name = "Màu sắc" },
                                                                           new ProductAttribute { ID = 2, Name = "Kích cỡ" });

            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue { ID = 1, AttributeID = 1, Name = "Đỏ" },
                                                            new AttributeValue { ID = 2, AttributeID = 1, Name = "Xanh" },
                                                            new AttributeValue { ID = 3, AttributeID = 2, Name = "S" },
                                                            new AttributeValue { ID = 4, AttributeID = 2, Name = "M" },
                                                            new AttributeValue { ID = 5, AttributeID = 2, Name = "L" },
                                                            new AttributeValue { ID = 6, AttributeID = 2, Name = "XL" });












        }



    }
}
