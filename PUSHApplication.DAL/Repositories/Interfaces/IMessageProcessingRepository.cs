using PUSHApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUSHApplication.DAL.Repositories.Interfaces
{
    public interface IMessageProcessingRepository
    {
        Task AddMessageAsync(Message message, IList<string> phoneNumbers);
        Task<List<string>> GetTokensByPhoneNumbersAsync(IList<string> phoneNumbers);
    }
}
