using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueueConfiguration
{
    public class ConfigurationCommon: IDisposable
    {
        protected readonly ConnectionFactory connectionFactory;
        protected readonly IConnection connection;
        protected readonly IModel channel;
        protected readonly IBasicProperties properties;
        protected readonly string queueName;

        public ConfigurationCommon(string hostName, string queueName)
        {
            connectionFactory = new ConnectionFactory() { HostName = hostName };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            this.queueName = queueName;

            properties = channel.CreateBasicProperties();

            QueueDeclare();
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }

        private void QueueDeclare()
        {
            channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            properties.Persistent = true;
        }
    }
}
