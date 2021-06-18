using System.ComponentModel.DataAnnotations;

namespace Registration.BLL.Dto
{
    public class MobileAppDto
    {
        [Required(ErrorMessage = "Не указан токен")]
        public string FirebaseToken { get; set; }

        [Required(ErrorMessage = "Не указан телефонный номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указана версия приложения")]
        public string AppVersion { get; set; }
    }
}
