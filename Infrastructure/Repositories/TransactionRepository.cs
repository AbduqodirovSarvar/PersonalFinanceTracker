using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext dbContext, IRedisCacheService redisCacheService) : GenericRepository<Transaction>(dbContext, redisCacheService), ITransactionRepository
    {
    }
}
