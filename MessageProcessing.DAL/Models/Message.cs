using System;
using System.Collections.Generic;
using System.Text;

namespace MessageProcessing.DAL.Models
{
    public class Message
    {
        public string Header { get; }
        public string Text { get; }
        public DateTime MessageSendTime { get; }
        public IList<string> PhoneNumbers { get; }
    }
}
