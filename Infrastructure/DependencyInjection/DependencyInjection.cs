using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Infrastructure.Repositories;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepositoy, UserRepository>();
            services.AddScoped<ICategoryRepositoy, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.AddInterceptors(interceptor);
            });


            var redisConnection = configuration.GetConnectionString("Redis");
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection!));

            return services;
        }
    }
}
