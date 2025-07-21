using Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<TransactionProfile>();
                cfg.AddProfile<AuditLogProfile>();
            });

            return services;
        }
    }
}
