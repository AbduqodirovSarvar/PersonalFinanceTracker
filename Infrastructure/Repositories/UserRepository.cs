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
    public class UserRepository(DbContext context, IRedisCacheService redisCacheService) : GenericRepository<User>(context, redisCacheService), IUserRepositoy
    {
    }
}
