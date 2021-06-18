using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.BLL.Services.Interfaces
{
    public interface IMessageProcessingService : IDisposable
    {
        Task<bool> MessageProcessingAsync(string messageJson);
        void Start();
    }
}
