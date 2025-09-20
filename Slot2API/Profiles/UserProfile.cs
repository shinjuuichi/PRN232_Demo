using AutoMapper;
using Slot2API.DTOs;
using Slot2API.Models;

namespace Slot2API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, GetUserDTO>().ReverseMap();
        }
    }
}
