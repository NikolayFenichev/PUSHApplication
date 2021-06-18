using MessageQueueConfiguration;
using MessageReciever.BLL.Models;
using MessageReciever.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MessageReciever.BLL.Services
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IProducerConfiguration _rabbitProducerConfiguration;
        public SendMessageService(IProducerConfiguration rabbitProducerConfiguration)
        {
            _rabbitProducerConfiguration = rabbitProducerConfiguration;
        }

        public void Send(MessageDto message)
        {
            var json = JsonSerializer.Serialize<MessageDto>(message);

            _rabbitProducerConfiguration.Send(json);
        }
        public void Dispose()
        {
            _rabbitProducerConfiguration.Dispose();
        }
    }
}
