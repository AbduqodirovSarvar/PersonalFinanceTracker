using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context, IRedisCacheService redisCacheService)
        : GenericRepository<User>(context, redisCacheService), IUserRepositoy
    {
        public override async Task<User?> GetAsync(Expression<Func<User, bool>> predicate)
        {
            var cacheKey = $"GetAsync:{typeof(User).Name}:{predicate}";

            try
            {
                var cached = await _cacheService.GetAsync<User>(cacheKey);
                if (cached is not null) return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            var entity = await _dbSet.AsNoTracking()
                                     .Include(x => x.AuditLogs)
                                     .Include(x => x.Categories)
                                     .Include(x => x.Transactions)
                                     .FirstOrDefaultAsync(predicate);

            if (entity is not null)
            {
                try
                {
                    await _cacheService.SetAsync(cacheKey, entity, TimeSpan.FromMinutes(60));
                }
                catch
                {
                    Console.WriteLine("Redis cache ishlamadi, davom etamiz");
                }
            }

            return entity;
        }

        public override async Task<List<User>> GetAllAsync(Expression<Func<User, bool>>? predicate = null)
        {
            var cacheKey = predicate is null
                ? $"GetAllAsync:{typeof(User).Name}"
                : $"GetAllAsync:{typeof(User).Name}:{predicate}";

            try
            {
                var cached = await _cacheService.GetAsync<List<User>>(cacheKey);
                if (cached is not null) return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            IQueryable<User> query = _dbSet.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            var result = await query
                .Include(x => x.AuditLogs)
                .Include(x => x.Categories)
                .Include(x => x.Transactions)
                .ToListAsync();

            try
            {
                await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(60));
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            return result;
        }

        public override async Task<(List<User> Data, int TotalItems)> GetPaginatedAsync(
            Expression<Func<User, bool>>? predicate = null,
            int pageIndex = 0,
            int pageSize = 10,
            Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null)
        {
            var predicateKey = predicate?.ToString() ?? "all";
            var orderByKey = orderBy?.Method.Name ?? "default";
            var cacheKey = $"Paginated:{typeof(User).Name}:{predicateKey}:{orderByKey}:{pageIndex}:{pageSize}";

            try
            {
                var cached = await _cacheService.GetAsync<(List<User> Data, int TotalItems)>(cacheKey);
                if ((cached.Data?.Any() ?? false) || cached.TotalItems > 0)
                    return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            IQueryable<User> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            int totalItems = await query.CountAsync();

            if (orderBy is not null)
                query = orderBy(query);

            var data = await query
                .Include(x => x.AuditLogs)
                .Include(x => x.Categories)
                .Include(x => x.Transactions)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            var result = (data, totalItems);

            try
            {
                await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(60));
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            return result;
        }

        public override IQueryable<User> Query(Expression<Func<User, bool>>? predicate = null)
        {
            IQueryable<User> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            return query
                .Include(x => x.AuditLogs)
                .Include(x => x.Categories)
                .Include(x => x.Transactions);
        }
    }
}