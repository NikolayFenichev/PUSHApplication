using MessageProcessing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.DAL.Repositories.Interfaces
{
    public interface IMessageProcessingRepository
    {
        Task<bool> AddMessageAsync(Message message);
    }
}
