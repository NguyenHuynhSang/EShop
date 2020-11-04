using DatingApp.API.Dtos;
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

            CreateMap<ProductVersionImage, ProductVersionImageDto>();
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
                     opt.MapFrom(src => src.AttributeValue.ID);
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
            CreateMap<Product, ProductForListDto>();
            CreateMap<Product, ProductForListDto>()
                .ForMember(dest => dest.ID, opt => opt.Ignore());


            CreateMap<Item, ExchangeRateDongA>()
                .ForMember(dest => dest.type, opt => opt.NullSubstitute("N/A"))
                .ForMember(dest => dest.muack, act => act.MapFrom(src => String.IsNullOrEmpty(src.muack) ? 0 : float.Parse(src.muack)))
                .ForMember(dest => dest.muatienmat, act => act.MapFrom(src => String.IsNullOrEmpty(src.muatienmat) ? 0 : float.Parse(src.muatienmat)))
                .ForMember(dest => dest.banck, act => act.MapFrom(src => String.IsNullOrEmpty(src.banck) ? 0 : float.Parse(src.banck)))
                .ForMember(dest => dest.bantienmat, act => act.MapFrom(src => String.IsNullOrEmpty(src.bantienmat) ? 0 : float.Parse(src.bantienmat)));


            //ignore ID , entity need to get from db and map to update entity
            // track issue cause by ef core
            CreateMap<ExchangeRateDongA, ExchangeRateDongA>()
                 .ForMember(dest => dest.ID, opt => opt.Ignore());


            CreateMap<ExchangeRateDongA, ExchangeRateDongA>()
               .ForMember(dest => dest.ID, opt => opt.Ignore());
            CreateMap<ProductCatalog, ProductCatalog>()
               .ForMember(dest => dest.ID, opt => opt.Ignore());


            CreateMap<IEnumerable<ProductVersionViewModel>, IEnumerable<ProductVersion>>();
            CreateMap<IEnumerable<ProductVersion>, IEnumerable<ProductVersionViewModel>>();



            CreateMap<Models.Attribute, Models.Attribute>()
                  .ForMember(dest => dest.ID, opt => opt.Ignore());



        }
    }
}
