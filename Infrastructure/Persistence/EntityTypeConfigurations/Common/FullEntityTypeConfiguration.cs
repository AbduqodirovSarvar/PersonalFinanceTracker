using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Common
{
    public class FullEntityTypeConfiguration<TEntity> : AudiTableEntityTypeConfiguration<TEntity>
        where TEntity : FullEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.IsDeleted).IsRequired(true);
        }
    }
}
