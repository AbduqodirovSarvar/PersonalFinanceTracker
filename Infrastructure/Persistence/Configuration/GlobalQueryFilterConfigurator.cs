using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Configuration
{
    public static class GlobalQueryFilterConfigurator
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(FullEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var isDeletedProperty = Expression.Property(parameter, nameof(FullEntity.IsDeleted));
                    var filter = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                    var lambda = Expression.Lambda(filter, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
    }
}
