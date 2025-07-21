using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuditLogRepository(AppDbContext context) : IAuditLogRepository
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<AuditLog> _dbSet = context.Set<AuditLog>();

        public async Task<AuditLog> CreateAsync(AuditLog entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteAsync(AuditLog entity)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLog> UpdateAsync(AuditLog entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AuditLog?> GetAsync(Expression<Func<AuditLog, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Include(x => x.User)
                               .FirstOrDefaultAsync(e => predicate.Compile()(e));
        }

        public virtual async Task<List<AuditLog>> GetAllAsync(Expression<Func<AuditLog, bool>>? predicate = null)
        {
            IQueryable<AuditLog> query = _dbSet.AsNoTracking().Include(x => x.User);

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public virtual async Task<(List<AuditLog> Data, int TotalItems)> GetPaginatedAsync(
            Expression<Func<AuditLog, bool>>? predicate = null,
            int pageIndex = 0,
            int pageSize = 10,
            Func<IQueryable<AuditLog>, IOrderedQueryable<AuditLog>>? orderBy = null)
        {
            IQueryable<AuditLog> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            int totalItems = await query.CountAsync();

            if (orderBy is not null)
                query = orderBy(query);

            var data = await query.Include(x => x.User).Skip(pageIndex * pageSize)
                                  .Take(pageSize)
                                  .AsNoTracking()
                                  .ToListAsync();

            return (data, totalItems);
        }

        public virtual IQueryable<AuditLog> Query(Expression<Func<AuditLog, bool>>? predicate = null)
        {
            IQueryable<AuditLog> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            return query.AsQueryable().Include(x => x.User);
        }
    }
}
