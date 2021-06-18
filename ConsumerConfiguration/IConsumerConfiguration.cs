using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessingConsumerConfiguration
{
    public interface IConsumerConfiguration
    {
        void Configure(Func<string, Task<bool>> function);
    }
}
