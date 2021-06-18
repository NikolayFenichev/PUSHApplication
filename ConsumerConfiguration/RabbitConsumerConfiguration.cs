using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessingConsumerConfiguration
{
    public class RabbitConsumerConfiguration : IConsumerConfiguration
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IBasicProperties _properties;
        private readonly string _queueName;

        public RabbitConsumerConfiguration(string hostName, string queueName)
        {
            _connectionFactory = new ConnectionFactory() { HostName = hostName };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _queueName = queueName;

            _properties = _channel.CreateBasicProperties();

            QueueDeclare();
        }

        public void Configure(Func<string, Task<bool>> function)
        {
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var result = await function.Invoke(message);

                if(result)
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);
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
