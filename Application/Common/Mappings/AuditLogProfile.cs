using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class AuditLogProfile : Profile
    {
        public AuditLogProfile()
        {
            CreateMap<AuditLog, AudiLogViewModel>().ReverseMap();
        }
    }
}
