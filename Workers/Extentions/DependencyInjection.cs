using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Interfaces;
using Workers.MessageBroker;
using Workers.Models;
using Workers.Services;

namespace Workers.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkers(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));

            services.Configure<RabbitMqConfig>(opt =>
            {
                opt.Uri = configuration.GetConnectionString("RabbitMq")!;
            });

            services.AddScoped<IRabbitMqConsumer, RabbitMqConsumer>();
            services.AddScoped<IExcelService, ExcelService>();

            services.AddHostedService<Worker>();
            return services;
        }
    }
}
