using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using Application.Interfaces;
using RabbitMQ.Client;
using Infrastructure.MessageBroker.Settings.RabbitMqConfigs;

namespace Infrastructure.MessageBroker.Producer
{
    public class RabbitMqProducer(IOptions<RabbitMqConfig> config) : IMessageProducer, IAsyncDisposable
    {
        private readonly RabbitMqConfig _config = config.Value;
        private IConnection? _connection;
        private IChannel? _channel;

        public async Task InitializeAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _config.Host,
                Port = _config.Port,
                UserName = _config.Username,
                Password = _config.Password
            };

            const int maxRetries = 5;
            var delay = TimeSpan.FromSeconds(5);

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    _connection = await factory.CreateConnectionAsync();
                    _channel = await _connection.CreateChannelAsync();

                    await _channel.QueueDeclareAsync(queue: _config.QueueName,
                                                     durable: false,
                                                     exclusive: false,
                                                     autoDelete: false,
                                                     arguments: null);
                    return;
                }
                catch (Exception ex)
                {
                    if (attempt == maxRetries)
                        throw;

                    Console.WriteLine($"RabbitMQ ulanish urunishi {attempt} muvaffaqiyatsiz. Qayta urinilmoqda...");
                    await Task.Delay(delay);
                }
            }

            await Task.CompletedTask;
        }

        public async Task SendMessage<T>(T message)
        {
            if (_channel == null)
                throw new InvalidOperationException("RabbitMQ channel is not initialized. Call InitializeAsync() before sending messages.");

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _config.QueueName, body: body);
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel is not null)
                await _channel.DisposeAsync();

            if (_connection is not null)
                await _connection.DisposeAsync();
        }
    }
}
