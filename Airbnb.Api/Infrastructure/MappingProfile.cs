using Airbnb.Api.Models;
using Airbnb.Data.Models;
using AutoMapper;

namespace Airbnb.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<User, UserDto>();
        }
    }
}
