using Microsoft.Extensions.Caching.Distributed;
using PUSHApplication.DAL.Repositories.Interfaces;
using System;

namespace PUSHApplication.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private PUSHApplicationContext _db;
        private IDistributedCache _cache;
        private IRegistrationRepository _registrationRepository;
        private IMessageProcessingRepository _messageProcessingRepository;
        private IStatisticRepository _statisticRepository;

        private bool disposed = false;

        public IRegistrationRepository RegistrationRepository
        {
            get
            {
                if (_registrationRepository == null)
                    _registrationRepository = new RegistrationRepository(_db, _cache);

                return _registrationRepository;
            }
        }

        public IMessageProcessingRepository MessageProcessingRepository
        {
            get
            {
                if (_messageProcessingRepository == null)
                    _messageProcessingRepository = new MessageProcessingRepository(_db, _cache);

                return _messageProcessingRepository;
            }
        }

        public IStatisticRepository StatisticRepository
        {
            get
            {
                if (_statisticRepository == null)
                    _statisticRepository = new StatisticRepository(_db, _cache);

                return _statisticRepository;
            }
        }

        public UnitOfWork(PUSHApplicationContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
