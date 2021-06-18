using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Paging;
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
    public class StatisticRepository : IStatisticRepository
    {
        private PUSHApplicationContext _db;
        private IDistributedCache _cache;

        public StatisticRepository(PUSHApplicationContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task<List<VersionInfo>> GetRegisteredVersionsAsync()
        {
            var versionsInfo = new List<VersionInfo>();
            var mobileAppsJson = await _cache.GetStringAsync(RedisKeysHelper.Registrations);
            var mobileApps = (mobileAppsJson == null) ? default
                : JsonSerializer.Deserialize<IEnumerable<MobileApp>>(mobileAppsJson);

            if (mobileApps == null)
            {
                mobileApps = await _db.MobileApps.ToListAsync();
                if (mobileApps != null && mobileApps.Any())
                {
                    await _cache.SetStringAsync(RedisKeysHelper.Registrations, JsonSerializer.Serialize(mobileApps), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                    });
                }
            }

            if(mobileApps != null && mobileApps.Any())
            {
                versionsInfo = mobileApps.GroupBy(a => a.AppVersion)
                    .Select((app, count) => new VersionInfo { 
                        Version = app.Key, 
                        CountRegistration = count, 
                        PhoneNumbers = app.Select(a => a.PhoneNumber).ToList() 
                    }).ToList();
            }

            return versionsInfo;
        }

        public async Task<PagedList<Message>> GetMessagesByPhoneNumberAsync(PageParameters pageParameters, string phoneNumber)
        {
            var messagesToPhonesJson = await _cache.GetStringAsync(RedisKeysHelper.PhoneNumberMessages);
            var messagesToPhones = (messagesToPhonesJson == null) ? default
                : JsonSerializer.Deserialize<IEnumerable<MessageToPhoneNumber>>(messagesToPhonesJson,
                    new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve });

            if (messagesToPhones == null)
            {
                messagesToPhones = await _db.MessageToPhoneNumbers.Include(n => n.Message).ToListAsync();

                if (messagesToPhones != null && messagesToPhones.Any())
                {
                    await _cache.SetStringAsync(RedisKeysHelper.PhoneNumberMessages, JsonSerializer.Serialize(messagesToPhones,
                    new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve }), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                    });
                }
            }

            var messages = messagesToPhones
                .Where(n => n.MobileAppPhoneNumber == phoneNumber)
                .Select(n => n.Message)
                .ToList();

            return PagedListExtention<Message>.ToPagedList(messages, pageParameters.PageNumber, pageParameters.PageSize);
        }
    }
}
