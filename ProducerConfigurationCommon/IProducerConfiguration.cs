using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConfigurationCommon
{
    public interface IProducerConfiguration: IDisposable
    {
        void Send(string message);
    }
}
