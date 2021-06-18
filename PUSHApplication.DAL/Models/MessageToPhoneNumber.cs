using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PUSHApplication.DAL.Models
{
    public class MessageToPhoneNumber
    {
        public string MobileAppPhoneNumber { get; set; }
        public long MessageId { get; set; }

        public MobileApp MobileApp { get; set; }
        public Message Message { get; set; }
    }
}
