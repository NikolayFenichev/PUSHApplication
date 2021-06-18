using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statistic.Web.Models
{
    class VersionInfoViewModel
    {
        public string Version { get; set; }
        public int CountRegistration { get; set; }
        public IList<string> PhoneNumbers { get; set; }
    }
}
