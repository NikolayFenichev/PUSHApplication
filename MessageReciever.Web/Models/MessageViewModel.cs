using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessageReciever.Web.Models
{
    public class MessageViewModel
    {
        [Required(ErrorMessage = "Не указан заголовок сообщения")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Не указан текст сообщения")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Не указано время отправки сообщения")]
        public DateTime MessageSendTime { get; set; }

        [Required(ErrorMessage = "Не указан список номеров телефонов для отправки сообщения")]
        public IList<string> PhoneNumbers { get; set; }
    }
}
