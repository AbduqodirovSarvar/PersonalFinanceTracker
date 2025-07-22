using Workers.Interfaces;

namespace Workers
{
    public class Worker(ILogger<Worker> logger, IRabbitMqConsumer rabbitMqConsumer) : BackgroundService
    {
        private readonly ILogger<Worker> _logger = logger;
        private readonly IRabbitMqConsumer _rabbitMqConsumer = rabbitMqConsumer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🔄 RabbitMQ consumer starting...");

            await _rabbitMqConsumer.StartConsumingAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
