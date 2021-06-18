using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.BLL.Configuration
{
    public interface IFirebaseKey
    {
        public string ServerKey { get; }
        public string SenderId { get; }
    }
}
