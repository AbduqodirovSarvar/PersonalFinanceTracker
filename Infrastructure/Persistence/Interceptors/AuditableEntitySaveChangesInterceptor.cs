using Application.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Infrastructure.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor(
    ICurrentUserService currentUserService
) : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var userId = currentUserService.UserId;
            var auditLogs = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries<FullEntity>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    var auditLog = new AuditLog
                    {
                        EntityName = entry.Metadata.GetTableName() ?? "Unknown",
                        Action = entry.State.ToString(),
                        UserId = userId ?? null,
                        CreatedAt = DateTime.UtcNow,
                        OldValue = entry.State == EntityState.Modified || entry.State == EntityState.Deleted
                            ? SerializeOriginalValues(entry)
                            : null,
                        NewValue = entry.State == EntityState.Added || entry.State == EntityState.Modified
                            ? SerializeCurrentValues(entry)
                            : null,
                        EntityId = entry.Entity.Id,
                    };

                    auditLogs.Add(auditLog); // add to temp list instead of DbContext
                }
            }

            // Now add to context after enumeration is done
            context.Set<AuditLog>().AddRange(auditLogs);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private string SerializeOriginalValues(EntityEntry entry)
        {
            var dict = entry.OriginalValues.Properties.ToDictionary(
                p => p.Name,
                p => entry.OriginalValues[p]?.ToString());
            return JsonSerializer.Serialize(dict);
        }

        private string SerializeCurrentValues(EntityEntry entry)
        {
            var dict = entry.CurrentValues.Properties.ToDictionary(
                p => p.Name,
                p => entry.CurrentValues[p]?.ToString());
            return JsonSerializer.Serialize(dict);
        }
    }
}
