using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationService.Web.Models
{
    public class MobileAppViewModel
    {
        [Required(ErrorMessage = "Не указан токен")]
        public string FirebaseToken { get; set; }

        [Required(ErrorMessage = "Не указан телефонный номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указана версия приложения")]
        public string AppVersion { get; set; }
    }
}
