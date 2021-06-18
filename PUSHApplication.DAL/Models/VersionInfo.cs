using System;
using System.Collections.Generic;
using System.Text;

namespace PUSHApplication.DAL.Models
{
    public class VersionInfo
    {
        public string Version { get; set; }
        public int CountRegistration { get; set; }
        public IList<string> PhoneNumbers { get; set; }
    }
}
