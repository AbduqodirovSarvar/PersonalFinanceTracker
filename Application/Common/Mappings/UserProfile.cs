using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
