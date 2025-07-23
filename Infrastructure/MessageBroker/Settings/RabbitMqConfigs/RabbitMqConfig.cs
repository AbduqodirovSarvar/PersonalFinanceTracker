using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MessageBroker.Settings.RabbitMqConfigs
{
    public class RabbitMqConfig
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "export-statistics";

        public string Uri { get; set; } = string.Empty;
    }
}
