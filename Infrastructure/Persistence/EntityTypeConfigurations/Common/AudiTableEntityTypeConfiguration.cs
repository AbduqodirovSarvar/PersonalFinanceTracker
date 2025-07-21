using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Common
{
    public class AudiTableEntityTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity>
        where TEntity : AudiTableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
