using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.DefaultData;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableInterceptor
        ) : DbContext(options)
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor = auditableInterceptor;

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

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

        public async Task SeedAsync(IHashService hashService)
        {
            if (!Users.Any())
            {
                var users = DefaultUserData.Users(hashService);
                foreach (var user in users)
                {
                    if (!Users.Any(x => x.UserName == user.UserName || x.Email == user.Email))
                    {
                        Users.Add(user);
                    }
                }
                await SaveChangesAsync();
            }

        }

    }
}
