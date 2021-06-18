using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PUSHApplication.DAL.Models;
using PUSHApplication.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PUSHApplication.DAL.Repositories
{
    public class MessageProcessingRepository : IMessageProcessingRepository
    {
        private IDistributedCache _cache;
        private PUSHApplicationContext _db;

        public MessageProcessingRepository(PUSHApplicationContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task AddMessageAsync(Message message, IList<string> phoneNumbers)
        {
            var messageToPhoneNumbers = new List<MessageToPhoneNumber>();
            foreach (var phoneNumber in phoneNumbers)
            {
                messageToPhoneNumbers.Add(new MessageToPhoneNumber
                {
                    MobileAppPhoneNumber = phoneNumber
                });
            }

            if(message != null)
            {
                message.MessageToPhoneNumbers = messageToPhoneNumbers;
                _db.Messages.Add(message);

                await _db.SaveChangesAsync();

                await _cache.RemoveAsync(RedisKeysHelper.PhoneNumberMessages);
            }
        }

        public async Task<List<string>> GetTokensByPhoneNumbersAsync(IList<string> phoneNumbers)
        {
            return await _db.MobileApps.Where(m => phoneNumbers.Contains(m.PhoneNumber)).Select(m => m.FirebaseToken).ToListAsync();
        }
    }
}
