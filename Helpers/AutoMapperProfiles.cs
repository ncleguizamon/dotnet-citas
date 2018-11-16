using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.models;
namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, userForListDto>().ForMember(dest => dest.Photourl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });

            CreateMap<User, UserForDetaledDto>().ForMember(dest => dest.Photourl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });;
            
            CreateMap<Photo, PhotosForDetailedDto>();
        }

    }
}