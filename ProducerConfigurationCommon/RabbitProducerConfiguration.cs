using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConfigurationCommon
{
    public class RabbitProducerConfiguration : IProducerConfiguration
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IBasicProperties _properties;
        private readonly string _queueName;

        public RabbitProducerConfiguration(string hostName, string queueName)
        {
            _connectionFactory = new ConnectionFactory() { HostName = hostName };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _queueName = queueName;

            _properties = _channel.CreateBasicProperties();

            QueueDeclare();
        }

        public void Send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: _properties,
                                 body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        private void QueueDeclare()
        {
            _channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            _properties.Persistent = true;
        }
    }
}
