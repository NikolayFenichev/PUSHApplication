using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statistic.Web.Models
{
    public class MessageViewModel
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public DateTime MessageSendTime { get; set; }
    }
}
