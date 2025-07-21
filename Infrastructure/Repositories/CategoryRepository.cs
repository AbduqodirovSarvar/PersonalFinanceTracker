using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository(DbContext context, IRedisCacheService redisCacheService) : GenericRepository<Category>(context, redisCacheService), ICategoryRepositoy
    {
    }
}
