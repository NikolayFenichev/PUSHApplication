using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueueConfiguration
{
    public class RabbitProducerConfiguration : ConfigurationCommon, IProducerConfiguration
    {
        public RabbitProducerConfiguration(string hostName, string queueName): base(hostName, queueName)
        {
        }

        public void Send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: properties,
                                 body: body);
        }

        
    }
}
