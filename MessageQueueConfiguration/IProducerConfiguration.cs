using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueueConfiguration
{
    public interface IProducerConfiguration: IDisposable
    {
        void Send(string message);
    }
}
