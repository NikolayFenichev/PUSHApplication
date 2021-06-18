using Paging;
using Statistic.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.BLL.Services.Interfaces
{
    public interface IStatisticService: IDisposable
    {
        Task<List<VersionInfoDto>> GetRegisteredVersionsAsync();
        Task<PagedList<MessageDto>> GetMessagesByPhoneNumberAsync(PageParameters pageParameters, string phoneNumber);
    }
}
