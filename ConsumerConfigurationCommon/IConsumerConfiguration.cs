using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerConfigurationCommon
{
    public interface IConsumerConfiguration
    {
        void Configure(Func<string, Task<bool>> function);
    }
}
