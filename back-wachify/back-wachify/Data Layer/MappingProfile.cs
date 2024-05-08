using AutoMapper;
using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Dto;
using back_wachify.Model;

namespace back_wachify.Data_Layer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
