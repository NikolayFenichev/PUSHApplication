using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    public interface IProducerConfiguration : IDisposable
    {
        void Send(string message);
    }
}