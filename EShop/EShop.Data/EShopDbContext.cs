using EShop.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data
{
    public class EShopDbContext:DbContext
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ContentCategory> ContentCategories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentTag> contentTags { get; set; }

        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<Model.Models.Attribute> Attributes { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //Một bảng có 2 khóa chính phải sử dụng fluent API
        {
            modelBuilder.Entity<ContentTag>()
                .HasKey(o => new { o.TagID , o.ContentID});
        }
    }
}
