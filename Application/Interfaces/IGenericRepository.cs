using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<(List<TEntity> Data, int TotalItems)> GetPaginatedAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            int pageIndex = 0,
            int pageSize = 10,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);
    }
}
