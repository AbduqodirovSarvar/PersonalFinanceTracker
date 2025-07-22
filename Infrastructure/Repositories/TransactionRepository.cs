using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext dbContext, IRedisCacheService redisCacheService)
        : GenericRepository<Transaction>(dbContext, redisCacheService), ITransactionRepository
    {
        public override async Task<Transaction?> GetAsync(Expression<Func<Transaction, bool>> predicate)
        {
            var cacheKey = $"GetAsync:{typeof(Transaction).Name}:{predicate}";

            try
            {
                var cached = await _cacheService.GetAsync<Transaction>(cacheKey);
                if (cached is not null) return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            var entity = await _dbSet.AsNoTracking()
                                     .Include(x => x.Category)
                                     .Include(x => x.User)
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

        public override async Task<List<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>>? predicate = null)
        {
            var cacheKey = predicate is null
                ? $"GetAllAsync:{typeof(Transaction).Name}"
                : $"GetAllAsync:{typeof(Transaction).Name}:{predicate}";

            try
            {
                var cached = await _cacheService.GetAsync<List<Transaction>>(cacheKey);
                if (cached is not null) return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            IQueryable<Transaction> query = _dbSet.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            var result = await query
                                .Include(x => x.Category)
                                .Include(x => x.User)
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

        public override async Task<(List<Transaction> Data, int TotalItems)> GetPaginatedAsync(
            Expression<Func<Transaction, bool>>? predicate = null,
            int pageIndex = 0,
            int pageSize = 10,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>? orderBy = null)
        {
            var predicateKey = predicate?.ToString() ?? "all";
            var orderByKey = orderBy?.Method.Name ?? "default";
            var cacheKey = $"Paginated:{typeof(Transaction).Name}:{predicateKey}:{orderByKey}:{pageIndex}:{pageSize}";

            try
            {
                var cached = await _cacheService.GetAsync<(List<Transaction> Data, int TotalItems)>(cacheKey);
                if ((cached.Data?.Any() ?? false) || cached.TotalItems > 0)
                    return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            IQueryable<Transaction> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            int totalItems = await query.CountAsync();

            if (orderBy is not null)
                query = orderBy(query);

            var data = await query
                                .Include(x => x.Category)
                                .Include(x => x.User)
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

        public override IQueryable<Transaction> Query(Expression<Func<Transaction, bool>>? predicate = null)
        {
            IQueryable<Transaction> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            return query
                    .Include(x => x.Category)
                    .Include(x => x.User);
        }
    }
}