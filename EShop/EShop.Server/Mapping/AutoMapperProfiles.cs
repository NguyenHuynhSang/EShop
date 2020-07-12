using DatingApp.API.Dtos;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Server.Extension;
using EShop.Server.InputModel;
using EShop.Server.ViewModels;
using static EShop.Server.SchedulerTask.ExchangeRateTask;

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

            CreateMap<ProductInput, Product>();
            CreateMap<Product, ProductInput>();
            CreateMap<ProductVersionInput, ProductVersion>();
            CreateMap<ProductVersion, ProductVersionInput>();
            CreateMap<Item, ExchangeRateDongA>()
                .ForMember(dest => dest.type, opt => opt.NullSubstitute("N/A"))
                .ForMember(dest => dest.muack, act => act.MapFrom(src => String.IsNullOrEmpty(src.muack) ? 0 : float.Parse(src.muack)))
                .ForMember(dest => dest.muatienmat, act => act.MapFrom(src => String.IsNullOrEmpty(src.muatienmat) ? 0 : float.Parse(src.muatienmat)))
                .ForMember(dest => dest.banck, act => act.MapFrom(src => String.IsNullOrEmpty(src.banck) ? 0 : float.Parse(src.banck)))
                .ForMember(dest => dest.bantienmat, act => act.MapFrom(src => String.IsNullOrEmpty(src.bantienmat) ? 0 : float.Parse(src.bantienmat)));


            //ignore ID 
            CreateMap<ExchangeRateDongA, ExchangeRateDongA>()
                 .ForMember(dest => dest.ID, opt => opt.Ignore());
            CreateMap<IEnumerable<ProductVersionViewModel>, IEnumerable<ProductVersion>>();
            CreateMap<IEnumerable<ProductVersion>, IEnumerable<ProductVersionViewModel>>();

        }
    }
}
