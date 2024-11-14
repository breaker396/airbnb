using Airbnb.Api.Models;
using Airbnb.Data.Models;
using AutoMapper;

namespace Airbnb.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : string.Empty))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency != null ? src.Currency.Code : string.Empty))
                .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.Currency != null ? src.Currency.Name : string.Empty))
                .ForMember(dest => dest.CurrencySymbol, opt => opt.MapFrom(src => src.Currency != null ? src.Currency.Symbol : string.Empty))
                .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province != null ? src.Province.Name : string.Empty))
                ;
            CreateMap<User, UserDto>().ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
