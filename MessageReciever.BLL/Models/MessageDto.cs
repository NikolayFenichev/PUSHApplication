using System;
using System.Collections.Generic;
using System.Text;

namespace MessageReciever.BLL.Models
{
    public class MessageDto
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime MessageSendTime { get; set; }
        public IList<string> PhoneNumbers { get; set; }
    }
}
