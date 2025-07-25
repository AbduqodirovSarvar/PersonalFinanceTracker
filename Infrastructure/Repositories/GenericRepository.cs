﻿using Application.Interfaces;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context, IRedisCacheService cacheService) : IGenericRepository<TEntity>
        where TEntity : FullEntity
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
        protected readonly IRedisCacheService _cacheService = cacheService;

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var cacheKey = $"GetAsync:{typeof(TEntity).Name}:{predicate}";

            try
            {
                var cached = await _cacheService.GetAsync<TEntity>(cacheKey);
                if (cached is not null) return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

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

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            var cacheKey = predicate is null
                ? $"GetAllAsync:{typeof(TEntity).Name}"
                : $"GetAllAsync:{typeof(TEntity).Name}:{predicate}";

            try
            {
                var cached = await _cacheService.GetAsync<List<TEntity>>(cacheKey);
                if (cached is not null) return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (predicate is not null)
                query = query.Where(predicate);

            var result = await query.ToListAsync();

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

        public virtual async Task<(List<TEntity> Data, int TotalItems)> GetPaginatedAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            int pageIndex = 0,
            int pageSize = 10,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var predicateKey = predicate?.ToString() ?? "all";
            var orderByKey = orderBy?.Method.Name ?? "default";
            var cacheKey = $"Paginated:{typeof(TEntity).Name}:{predicateKey}:{orderByKey}:{pageIndex}:{pageSize}";

            try
            {
                var cached = await _cacheService.GetAsync<(List<TEntity> Data, int TotalItems)>(cacheKey);
                if ((cached.Data?.Any() ?? false) || cached.TotalItems > 0)
                    return cached;
            }
            catch
            {
                Console.WriteLine("Redis cache ishlamadi, davom etamiz");
            }

            IQueryable<TEntity> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            int totalItems = await query.CountAsync();

            if (orderBy is not null)
                query = orderBy(query);

            var data = await query.Skip(pageIndex * pageSize)
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

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            return query.AsQueryable();
        }
    }
}