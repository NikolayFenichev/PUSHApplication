using MessageProcessing.DAL.Models;
using MessageProcessing.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.DAL.Repositories
{
    public class MessageProcessingRepository : IMessageProcessingRepository
    {
        private MessageProcessingContext _db;

        public MessageProcessingRepository(MessageProcessingContext db)
        {
            _db = db;
        }

        public Task<bool> AddMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
