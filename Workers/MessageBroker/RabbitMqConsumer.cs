using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Workers.Interfaces;
using Workers.Models;

namespace Workers.MessageBroker
{
    public class RabbitMqConsumer(
        IExcelService excelService,
        IOptions<RabbitMqConfig> options) : IRabbitMqConsumer
    {
        private readonly IExcelService _excelService = excelService;
        private readonly RabbitMqConfig _config = options.Value;

        public async Task StartConsumingAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _config.Host,
                Port = _config.Port,
                UserName = _config.Username,
                Password = _config.Password,
                //Uri = new Uri(_config.Uri)
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _config.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var request = JsonSerializer.Deserialize<StatisticExportRequest>(message);
                    if (request is not null)
                    {
                        await _excelService.GenerateExcelFileAsync(request);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Xatolik: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: _config.QueueName,
                autoAck: true,
                consumer: consumer);

            Console.WriteLine("⏳ RabbitMQ queue listening started...");
        }

    }
}
