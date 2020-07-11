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
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(src => src.DateOfBirth.Age());
                });
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(src => src.DateOfBirth.Age());
                });
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForDetailDto>();
           
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<ProductInput, Product>();
            CreateMap<Product,ProductInput> ();
            CreateMap<ProductVersionInput, ProductVersion>();
            CreateMap<ProductVersion, ProductVersionInput>();
            CreateMap<Item, ExchangeRateDongA>();
           
            CreateMap<IEnumerable<ProductVersionViewModel>, IEnumerable<ProductVersion>>();
            CreateMap<IEnumerable<ProductVersion>, IEnumerable<ProductVersionViewModel>>();
         
        }
    }
}
