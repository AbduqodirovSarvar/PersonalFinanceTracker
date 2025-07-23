using Application.Interfaces;
using Application.Services;
using Infrastructure.Identity;
using Infrastructure.MessageBroker.Producer;
using Infrastructure.MessageBroker.Settings.RabbitMqConfigs;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHashService, HashService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepositoy, UserRepository>();
            services.AddScoped<ICategoryRepositoy, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IAuthService, AuthService>();    
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<IMessageProducer, RabbitMqProducer>();

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
