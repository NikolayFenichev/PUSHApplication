using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueConfiguration
{
    public class RabbitConsumerConfiguration : ConfigurationCommon, IConsumerConfiguration
    {
        public RabbitConsumerConfiguration(string hostName, string queueName): base(hostName, queueName)
        {
        }

        public void Configure(Func<string, Task<bool>> function)
        {
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var result = await function.Invoke(message);

                if (result)
                {
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);
        }
    }
}
