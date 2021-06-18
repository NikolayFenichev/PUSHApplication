using Paging;
using PUSHApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUSHApplication.DAL.Repositories.Interfaces
{
    public interface IStatisticRepository
    {
        Task<List<VersionInfo>> GetRegisteredVersionsAsync();
        Task<PagedList<Message>> GetMessagesByPhoneNumberAsync(PageParameters pageParameters, string phoneNumber);
    }
}
