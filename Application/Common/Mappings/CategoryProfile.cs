using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
