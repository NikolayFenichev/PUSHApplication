using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.BLL.Models
{
    public class FirebaseMessageDto
    {
        public List<string> FirebaseTokens { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
    }
}
