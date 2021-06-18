using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PUSHApplication.DAL.Models
{
    [Index("PhoneNumber")]
    public class MobileApp
    {
        [Key]
        public string PhoneNumber { get; set; }
        public string FirebaseToken { get; set; }
        public string AppVersion { get; set; }

        public List<MessageToPhoneNumber> MessageToPhoneNumbers { get; set; }
    }
}
