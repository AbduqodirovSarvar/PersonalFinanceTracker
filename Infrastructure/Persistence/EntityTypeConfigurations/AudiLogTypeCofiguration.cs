using Domain.Entities;
using Infrastructure.Persistence.EntityTypeConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class AudiLogTypeCofiguration : AudiTableEntityTypeConfiguration<AuditLog>
    {
        public override void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.User)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
