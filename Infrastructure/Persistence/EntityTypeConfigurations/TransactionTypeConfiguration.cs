using Domain.Entities;
using Infrastructure.Persistence.EntityTypeConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class TransactionTypeConfiguration : FullEntityTypeConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
