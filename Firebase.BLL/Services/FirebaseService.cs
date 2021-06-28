using Firebase.BLL.Configuration;
using Firebase.BLL.Models;
using Firebase.BLL.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MessageQueueConfiguration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Firebase.BLL.Services
{
    public class FirebaseService : IFirebaseService
    {
        private const int maxTokenCount = 1000;

        private IConsumerConfiguration _rabbitConsumerConfiguration;
        private ILogger<FirebaseService> _logger;
        private HttpClient _httpClient;
        private string _serverKey;
        private string _senderId;

        public FirebaseService(IConsumerConfiguration consumerConfiguration,
            ILogger<FirebaseService> logger, IFirebaseKey firebaseKey)
        {
            _serverKey = firebaseKey.ServerKey;
            _senderId = firebaseKey.SenderId;
            _logger = logger;

            _httpClient = new HttpClient();

            _rabbitConsumerConfiguration = consumerConfiguration;
        }

        public void Start()
        {
            _rabbitConsumerConfiguration.Configure(MessagePush);
        }

        public async Task<bool> MessagePush(string firebaseMessageDtoJson)
        {
            _logger.LogInformation($"{nameof(MessagePush)}: Принято новое сообщение");
            try
            {
                var firebaseMessageDto = JsonSerializer.Deserialize<FirebaseMessageDto>(firebaseMessageDtoJson);
                var tokenCount = firebaseMessageDto.FirebaseTokens.Count;

                List<string> tokens = null;

                for (int i = 0; i < tokenCount; i += maxTokenCount)
                {
                    if(tokenCount - i > maxTokenCount)
                    {
                        tokens = firebaseMessageDto.FirebaseTokens.GetRange(i, maxTokenCount);
                    }
                    else
                    {
                        tokens = firebaseMessageDto.FirebaseTokens.GetRange(i, tokenCount);
                    }
                    
                    var data = new
                    {
                        registration_ids = tokens,
                        notification = new { title = firebaseMessageDto.Header, body = firebaseMessageDto.Text }
                    };

                    var jsonBody = JsonSerializer.Serialize(data);


                    var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");

                    httpRequest.Headers.TryAddWithoutValidation("Authorization", $"key={_serverKey}");
                    httpRequest.Headers.TryAddWithoutValidation("Sender", $"id={_senderId}");
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var result = await _httpClient.SendAsync(httpRequest);

                    if (!result.IsSuccessStatusCode)
                    {
                        _logger.LogError($"{nameof(MessagePush)}: {result.StatusCode}");
                        return false;
                    }
                }

                _logger.LogInformation($"{nameof(MessagePush)}: PUSH уведомление отправлено в FCM");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessagePush)}: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
