using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.BLL.Configuration
{
    public class FirebaseKey : IFirebaseKey
    {
        public string ServerKey { get => _serverKey; }
        public string SenderId { get => _senderId; }

        private string _serverKey;
        private string _senderId;

        public FirebaseKey(string serverKey, string senderId)
        {
            _serverKey = serverKey;
            _senderId = senderId;
        }
    }
}
