using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueConfiguration
{
    public interface IConsumerConfiguration: IDisposable
    {
        void Configure(Func<string, Task<bool>> function);
    }
}
