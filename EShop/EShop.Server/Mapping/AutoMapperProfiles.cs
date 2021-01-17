﻿using EShop.Server.Server.Dtos;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Server.Extension;
using EShop.Server.ViewModels;
using static EShop.Server.SchedulerTask.ExchangeRateTask;
using EShop.Server.Dtos.Admin.ProductForList;
using EShop.Server.Dtos.Admin;
using EShop.Server.Server.Dtos.ProductForList;
using EShop.Server.Client.Dtos;
using EShop.Server.Client.Dtos.Customer;
using EShop.Server.Client.Dtos.Order;
using EShop.Server.Client.Dtos.Catalog;
using EShop.Server.Client.Dtos.ProductFilterParam;
using EShop.Server.Client.Dtos.Shipping;
using EShop.Server.Server.Dtos.Order;

namespace EShop.Server.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(src => src.DateOfBirth.Age());
                });
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(src => src.DateOfBirth.Age());
                });
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForDetailDto>();

            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<ProductVersionImage, ProductVersionImageDto>()
               .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url != "string" && src.Url!=""? src.Url: @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg"));
            CreateMap<AttributeValue, AttributeValueDto>();
            CreateMap<ProductVersionAttribute, ProductVersionAttributeDto>()
                 .ForMember(dest => dest.AttributeName, opt =>
                 {
                     opt.MapFrom(src => src.AttributeValue.Attribute.Name);
                 })
                 .ForMember(dest => dest.AtributeID, opt =>
                 {
                     opt.MapFrom(src => src.AttributeValue.AttributeID);
                 })
                 .ForMember(dest => dest.AttributeValueName, opt =>
                 {
                     opt.MapFrom(src => src.AttributeValue.Name);
                 })
                 .ForMember(dest => dest.AttributeValueID, opt =>
                 {
                     opt.MapFrom(src => src.AttributeValue.Id);
                 });


            CreateMap<ProductVersion, ProductVersionDto>()
                 .ForMember(dest => dest.ImageUrl, opt =>
                 {
                     opt.MapFrom(src => src.ProductVersionImages.FirstOrDefault(p => p.IsMain).Url);
                 })
                  .ForMember(dest => dest.ProductVersionImages, opt =>
                  {
                      opt.MapFrom(src => src.ProductVersionImages.Where(p => !p.IsMain));
                  })
                   .ForMember(dest => dest.ProductVersionAttributes, opt =>
                   {
                       opt.MapFrom(src => src.ProductVersionAttributes);
                   });
            CreateMap<ProductCatalog, ProductCatalogDto>();
            CreateMap<ProductVersionAttribute, ProductVersionAttributeDto>();
            CreateMap<Product, ProductForListDto>();
            CreateMap<Product, ProductForListVerDto>();

      

            CreateMap<ProductVersion, ProductVersionForListDto>()
                .ForMember(dest=>dest.MainImage,opt=>opt.MapFrom(src=> src.ProductVersionImages.Count()>0&& src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url!="string"?src.ProductVersionImages.FirstOrDefault(x=>x.IsMain==true).Url: @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg"))
            .ForMember(dest => dest.ProductVersionImages, opt => opt.MapFrom(src => src.ProductVersionImages.Count()>0? src.ProductVersionImages: new List<ProductVersionImage>() { new ProductVersionImage() { IsMain = true, Url = @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg" } }));

            CreateMap<Product, ProductForSaleDto>()
                 .ForMember(dest => dest.CatalogId, opt => opt.MapFrom(src => src.Catalog.Id))
                 .ForMember(dest => dest.ParentCatalogId, opt => opt.MapFrom(src => src.Catalog.ParentID))
                  .ForMember(dest => dest.CatalogName, opt => opt.MapFrom(src => src.Catalog.Name))
                            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.ProductComments.Count()==0?0:src.ProductComments.Average(x => x.Rating)));


            CreateMap<Product, ProductForSaleListDto>()
             .ForMember(dest => dest.CatalogId, opt => opt.MapFrom(src => src.Catalog.Id))
              .ForMember(dest => dest.ParentCatalogId, opt => opt.MapFrom(src => src.Catalog.ParentID))
              .ForMember(dest => dest.NumOfComments, opt => opt.MapFrom(src => src.ProductComments.Count()))
              .ForMember(dest => dest.CatalogName, opt => opt.MapFrom(src => src.Catalog.Name))
                        .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.ProductComments.Count() == 0 ? 0 : src.ProductComments.Average(x => x.Rating)));


            CreateMap<Product, ProductRelatedDto>()
            .ForMember(dest => dest.CatalogId, opt => opt.MapFrom(src => src.Catalog.Id))
                 .ForMember(dest => dest.ParentCatalogId, opt => opt.MapFrom(src => src.Catalog.ParentID))
                  .ForMember(dest => dest.CatalogName, opt => opt.MapFrom(src => src.Catalog.Name))
                            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.ProductComments.Count() == 0 ? 0 : src.ProductComments.Average(x => x.Rating)));

            CreateMap<ProductVersion, ProductVersionRelatedDto>()
            .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductVersionImages.Count() > 0 && src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url != "string" ? src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url : @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg"))
            .ForMember(dest => dest.ProductVersionImages, opt => opt.MapFrom(src => src.ProductVersionImages.Count() > 0 ? src.ProductVersionImages : new List<ProductVersionImage>() { new ProductVersionImage() { IsMain = true, Url = @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg" } }))
                .ForMember(dest => dest.RelativeProductVersions, opt => opt.MapFrom(src => src.Product.ProductVersions.Where(x => x.Id != src.Id)));



            CreateMap<ProductVersion, RelativeProductVersionDto>()
               .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url));

            CreateMap<Address, AddressForViewDto>()
                 .ForMember(dest => dest.WardCode, opt => opt.MapFrom(src => src.WardCode))
                 .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.Ward.District.DistrictName))
                 .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Ward.District.Province.ProvinceName))
              .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.Ward.WardName));
            CreateMap<AddressForInputDto, Address>();


            CreateMap<Customer, CustomerForViewDto>();
            CreateMap<Customer, CustomerForLoginDto>();
            CreateMap<Customer, CustomerForRegisterDto>();
            CreateMap<Customer, CustomerForDetailDto>();
            CreateMap<CustomerForUpdateDto, Customer>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Customer, CustomerForUpdateDto>();
            CreateMap<Customer, CustomerForOrderDto>();
            CreateMap<Order, OrderForListDto>()
                 .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.OrderDetails.Sum(x=>x.Price*x.Quantity)))
                 .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.OrderDetails.Sum(x=>x.Quantity)));

            CreateMap<ProductComment, ProductCommentDto>()
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<ProductVersion, ProductVersionForSaleDto>()
                 .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductVersionImages.Count() > 0 && src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url != "string" ? src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url : @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg"))
            .ForMember(dest => dest.ProductVersionImages, opt => opt.MapFrom(src => src.ProductVersionImages.Count() > 0 ? src.ProductVersionImages : new List<ProductVersionImage>() { new ProductVersionImage() { IsMain = true, Url = @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg" } }))
                .ForMember(dest => dest.RelativeProductVersions, opt => opt.MapFrom(src => src.Product.ProductVersions.Where(x => x.Id != src.Id)));


            CreateMap<ProductVersion, ProductVersionForSaleListDto>()
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductVersionImages.Count() > 0 && src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url != "string" ? src.ProductVersionImages.FirstOrDefault(x => x.IsMain == true).Url : @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg"))
           .ForMember(dest => dest.ProductVersionImages, opt => opt.MapFrom(src => src.ProductVersionImages.Count() > 0 ? src.ProductVersionImages : new List<ProductVersionImage>() { new ProductVersionImage() { IsMain = true, Url = @"http://res.cloudinary.com/eshop2020/image/upload/v1608746056/wsrmyveqzxb2p5yloub3.jpg" } }))
               .ForMember(dest => dest.RelativeProductVersions, opt => opt.MapFrom(src => src.Product.ProductVersions.Where(x => x.Id != src.Id)));



            CreateMap<ProductCatalog, ProductCatalogForMenuDto>()
                ;
            CreateMap<ProductCatalog, CatalogForFilterDto>()
               .ForMember(dest => dest.Total, act => act.MapFrom(src=>src.Products.Sum(x=>x.ProductVersions.Count())));
            CreateMap<AttributeValue, AttributeForFilterDto>();

            CreateMap<Item, ExchangeRateDongA>()
                .ForMember(dest => dest.type, opt => opt.NullSubstitute("N/A"))
                .ForMember(dest => dest.muack, act => act.MapFrom(src => String.IsNullOrEmpty(src.muack) ? 0 : float.Parse(src.muack)))
                .ForMember(dest => dest.muatienmat, act => act.MapFrom(src => String.IsNullOrEmpty(src.muatienmat) ? 0 : float.Parse(src.muatienmat)))
                .ForMember(dest => dest.banck, act => act.MapFrom(src => String.IsNullOrEmpty(src.banck) ? 0 : float.Parse(src.banck)))
                .ForMember(dest => dest.bantienmat, act => act.MapFrom(src => String.IsNullOrEmpty(src.bantienmat) ? 0 : float.Parse(src.bantienmat)));


            ///product dto block
            ///


            CreateMap<ProductVersionAttributeForCreateDto, Models.ProductVersionAttribute>();
            //     CreateMap<ProductVersionImageDto, Models.ProductVersionImage>();
            CreateMap<ProductVersionImagesForCreateDto, Models.ProductVersionImage>();
            CreateMap<ProductVesionForCreateDto, ProductVersion>()
               .ForMember(dest => dest.ProductVersionImages, opt => opt.MapFrom(src => src.ProductVersionImages))
               .ForMember(dest => dest.ProductVersionAttributes, opt => opt.MapFrom(src => src.ProductVersionAttributes));
            CreateMap<ProductForInputDto, Product>()
                .ForMember(dest => dest.ProductVersions, opt => opt.MapFrom(src => src.ProductVersions));
       

            //ignore Id , entity need to get from db and map to update entity
            // track issue cause by ef core
            CreateMap<ExchangeRateDongA, ExchangeRateDongA>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<ExchangeRateDongA, ExchangeRateDongA>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProductCatalog, ProductCatalog>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<IEnumerable<ProductVersionViewModel>, IEnumerable<ProductVersion>>();
            CreateMap<IEnumerable<ProductVersion>, IEnumerable<ProductVersionViewModel>>()
                ;



            CreateMap<Models.Attribute, Models.Attribute>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<OrderDetailForCheckOutDto, OrderDetail>();
            CreateMap<OrderForCheckOutDto, Order>();
         


        }
    }
}
