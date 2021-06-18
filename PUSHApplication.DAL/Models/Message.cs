using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PUSHApplication.DAL.Models
{
    public class Message
    {
        [Key]
        public long MessageId { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime MessageSendTime { get; set; }

        public List<MessageToPhoneNumber> MessageToPhoneNumbers { get; set; }
    }
}
