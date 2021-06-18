using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PUSHApplication.DAL.Models;
using PUSHApplication.DAL.Repositories.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PUSHApplication.DAL.Repositories
{
    class RegistrationRepository : IRegistrationRepository
    {
        private PUSHApplicationContext _db;
        private IDistributedCache _cache;

        public RegistrationRepository(PUSHApplicationContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task<MobileApp> RegistrationAsync(MobileApp mobileApp)
        {
            var result = _db.MobileApps.Add(mobileApp);
            await _db.SaveChangesAsync();

            await _cache.RemoveAsync(RedisKeysHelper.Registrations);

            return result.Entity;
        }

        public async Task<MobileApp> UnRegistrationAsync(string token)
        {
            var mobileAppToRemove = await _db.MobileApps.FindAsync(token);

            if(mobileAppToRemove != null)
            {
                _db.MobileApps.Remove(mobileAppToRemove);
                await _db.SaveChangesAsync();
            }

            await _cache.RemoveAsync(RedisKeysHelper.Registrations);

            return mobileAppToRemove;
        }
    }
}
