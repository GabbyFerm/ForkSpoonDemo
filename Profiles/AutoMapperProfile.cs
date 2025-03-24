using AutoMapper;
using ForkSpoonDemo.DTOs;
using ForkSpoonDemo.Models;

namespace ForkSpoonDemo.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map UpdateUserDto to User
            CreateMap<UpdateUserDto, User>();
        }
    }
}
