﻿// <auto-generated />
using System;
using EShop.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EShop.Server.Migrations
{
    [DbContext(typeof(EShopDbContext))]
    [Migration("20201107133925_add_table_slide")]
    partial class add_table_slide
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EShop.Server.Models.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Attribute");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kích cỡ"
                        });
                });

            modelBuilder.Entity("EShop.Server.Models.AttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttributeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("AttributeID");

                    b.ToTable("AttributeValue");

                    b.HasData(
                        new
                        {
                            Id = 10,
                            AttributeID = 1,
                            Name = "S"
                        },
                        new
                        {
                            Id = 11,
                            AttributeID = 1,
                            Name = "M"
                        },
                        new
                        {
                            Id = 12,
                            AttributeID = 1,
                            Name = "L"
                        },
                        new
                        {
                            Id = 13,
                            AttributeID = 1,
                            Name = "XL"
                        },
                        new
                        {
                            Id = 14,
                            AttributeID = 1,
                            Name = "XXL"
                        });
                });

            modelBuilder.Entity("EShop.Server.Models.BlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("EShop.Server.Models.Content", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryID")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Detail")
                        .HasColumnType("ntext");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("MetaDescriptions")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("MetaKeywords")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("MetaTitle")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("SEODescription")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOTitle")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOUrl")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("TopHot")
                        .HasColumnType("datetime2");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.Property<int>("Warranty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("EShop.Server.Models.ContentCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("MetaDescriptions")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("MetaKeywords")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("MetaTitle")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<long>("ParentID")
                        .HasColumnType("bigint");

                    b.Property<string>("SEODescription")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOTitle")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOUrl")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("ShowOnHome")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ContentCategory");
                });

            modelBuilder.Entity("EShop.Server.Models.ContentTag", b =>
                {
                    b.Property<string>("TagID")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long>("ContentID")
                        .HasColumnType("bigint");

                    b.HasKey("TagID", "ContentID");

                    b.ToTable("ContentTag");
                });

            modelBuilder.Entity("EShop.Server.Models.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Error");
                });

            modelBuilder.Entity("EShop.Server.Models.ExchangeRateDongA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("banck")
                        .HasColumnType("real");

                    b.Property<float>("bantienmat")
                        .HasColumnType("real");

                    b.Property<string>("imageurl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("muack")
                        .HasColumnType("real");

                    b.Property<float>("muatienmat")
                        .HasColumnType("real");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRateDongAs");
                });

            modelBuilder.Entity("EShop.Server.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("EShop.Server.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("categoryID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("EShop.Server.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("EShop.Server.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ApplyPromotion")
                        .HasColumnType("bit");

                    b.Property<int>("CatalogID")
                        .HasColumnType("int")
                        .HasMaxLength(500);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deliver")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<decimal?>("OriginalPrice")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("SEODescription")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOTitle")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOUrl")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("CatalogID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("EShop.Server.Models.ProductCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("SEODescription")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOTitle")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("SEOUrl")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("ProductCatalog");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = false,
                            Name = "Áo"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = false,
                            Name = "Quần"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = false,
                            Name = "Váy"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = false,
                            Name = "Áo thun",
                            ParentID = 1
                        },
                        new
                        {
                            Id = 5,
                            IsActive = false,
                            Name = "Áo sơ mi",
                            ParentID = 1
                        },
                        new
                        {
                            Id = 6,
                            IsActive = false,
                            Name = "Áo khoác",
                            ParentID = 1
                        },
                        new
                        {
                            Id = 7,
                            IsActive = false,
                            Name = "Quần tây",
                            ParentID = 2
                        },
                        new
                        {
                            Id = 8,
                            IsActive = false,
                            Name = "Quần jean",
                            ParentID = 2
                        },
                        new
                        {
                            Id = 9,
                            IsActive = false,
                            Name = "Quần kari",
                            ParentID = 2
                        },
                        new
                        {
                            Id = 10,
                            IsActive = false,
                            Name = "Quần short",
                            ParentID = 2
                        },
                        new
                        {
                            Id = 11,
                            IsActive = false,
                            Name = "Váy toàn thân",
                            ParentID = 3
                        },
                        new
                        {
                            Id = 12,
                            IsActive = false,
                            Name = "Váy quần",
                            ParentID = 3
                        });
                });

            modelBuilder.Entity("EShop.Server.Models.ProductVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<decimal>("PromotionPrice")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RemainingAmount")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductVersions");
                });

            modelBuilder.Entity("EShop.Server.Models.ProductVersionAttribute", b =>
                {
                    b.Property<int>("AttributeValueID")
                        .HasColumnType("int");

                    b.Property<int>("ProductVersionID")
                        .HasColumnType("int");

                    b.HasKey("AttributeValueID", "ProductVersionID");

                    b.HasIndex("ProductVersionID");

                    b.ToTable("ProductVersionAttributes");
                });

            modelBuilder.Entity("EShop.Server.Models.ProductVersionImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("ProductVersionID")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductVersionID");

                    b.ToTable("ProductVersionImage");
                });

            modelBuilder.Entity("EShop.Server.Models.SeedLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DataVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SeedLog");
                });

            modelBuilder.Entity("EShop.Server.Models.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("EShop.Server.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("EShop.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interests")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Introduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KnownAs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<string>("LookingFor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EShop.Server.Models.AttributeValue", b =>
                {
                    b.HasOne("EShop.Server.Models.Attribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Server.Models.Photo", b =>
                {
                    b.HasOne("EShop.Server.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Server.Models.Product", b =>
                {
                    b.HasOne("EShop.Server.Models.ProductCatalog", "Catalog")
                        .WithMany()
                        .HasForeignKey("CatalogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Server.Models.ProductVersion", b =>
                {
                    b.HasOne("EShop.Server.Models.Product", "Product")
                        .WithMany("ProductVersions")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Server.Models.ProductVersionAttribute", b =>
                {
                    b.HasOne("EShop.Server.Models.AttributeValue", "AttributeValue")
                        .WithMany("ProductVersionAttributes")
                        .HasForeignKey("AttributeValueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EShop.Server.Models.ProductVersion", "ProductVersion")
                        .WithMany("ProductVersionAttributes")
                        .HasForeignKey("ProductVersionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Server.Models.ProductVersionImage", b =>
                {
                    b.HasOne("EShop.Server.Models.ProductVersion", "ProductVersion")
                        .WithMany("ProductVersionImages")
                        .HasForeignKey("ProductVersionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
