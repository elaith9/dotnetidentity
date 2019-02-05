using AutoMapper;
using TestDotNetCoreTemplate.Models.Dto;
using TestTemplate.Models;

namespace TestDotNetCoreTemplate.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}