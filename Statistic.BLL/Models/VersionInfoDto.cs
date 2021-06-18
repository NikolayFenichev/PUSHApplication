using System;
using System.Collections.Generic;
using System.Text;

namespace Statistic.BLL.Models
{
    public class VersionInfoDto
    {
        public string Version { get; set; }
        public int CountRegistration { get; set; }
        public IList<string> PhoneNumbers { get; set; }
    }
}
