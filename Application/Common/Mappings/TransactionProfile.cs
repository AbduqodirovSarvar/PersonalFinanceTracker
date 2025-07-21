using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}
