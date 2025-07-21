using Application.Interfaces;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableInterceptor
        ) : DbContext(options)
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor = auditableInterceptor;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            GlobalQueryFilterConfigurator.Configure(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.AddInterceptors(_auditableInterceptor);
        }
    }
}
