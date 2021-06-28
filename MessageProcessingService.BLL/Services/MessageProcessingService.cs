using AutoMapper;
using MessageProcessing.BLL.Models;
using MessageProcessing.BLL.Services.Interfaces;
using MessageQueueConfiguration;
using Microsoft.Extensions.Logging;
using PUSHApplication.DAL.Models;
using PUSHApplication.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessageProcessing.BLL.Services
{
    public class MessageProcessingService: IMessageProcessingService
    {
        private IUnitOfWork _database;
        private IConsumerConfiguration _rabbitConsumerConfiguration;
        private IProducerConfiguration _rabbitProducerConfiguration;
        private ILogger<MessageProcessingService> _logger;

        public MessageProcessingService(IUnitOfWork uow, IConsumerConfiguration consumerConfiguration,
            IProducerConfiguration producerConfiguration, ILogger<MessageProcessingService> logger)
        {
            _database = uow;
            _rabbitConsumerConfiguration = consumerConfiguration;
            _rabbitProducerConfiguration = producerConfiguration;
            _logger = logger;
        }

        public void Start()
        {
            _rabbitConsumerConfiguration.Configure(MessageProcessingAsync);
        }

        public async Task<bool> MessageProcessingAsync(string messageJson)
        {
            _logger.LogInformation($"{nameof(MessageProcessingAsync)}: Принято новое сообщение");

            var messageDto = JsonSerializer.Deserialize<MessageDto>(messageJson);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<MessageDto, Message>()));
            var message = mapper.Map<Message>(messageDto);

            try
            {
                await AddMessageAsync(message, messageDto.PhoneNumbers);
                await SendMessage(messageDto);

                _logger.LogInformation($"{nameof(MessageProcessingAsync)}: Сообщение обработано и отправлено в сервис отправки сообщения в FCM");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageProcessingAsync)}: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            _database.Dispose();
        }


        private async Task AddMessageAsync(Message message, IList<string> phoneNumbers)
        {
            try
            {
                await _database.MessageProcessingRepository.AddMessageAsync(message, phoneNumbers);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(AddMessageAsync)}: {ex.Message}");
                throw;
            }
        }

        private async Task SendMessage(MessageDto messageDto)
        {
            var tokens = await _database.MessageProcessingRepository.GetTokensByPhoneNumbersAsync(messageDto.PhoneNumbers);

            var firebaseMessage = new FirebaseMessageDto()
            {
                FirebaseTokens = tokens,
                Header = messageDto.Header,
                Text = messageDto.Text
            };

            var firebaseMessageJson = JsonSerializer.Serialize<FirebaseMessageDto>(firebaseMessage);

            try
            {
                _rabbitProducerConfiguration.Send(firebaseMessageJson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SendMessage)}: {ex.Message}");
                throw;
            }
        }
    }
}
