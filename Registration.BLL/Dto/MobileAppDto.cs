using System.ComponentModel.DataAnnotations;

namespace Registration.BLL.Dto
{
    public class MobileAppDto
    {
        public string FirebaseToken { get; set; }

        public string PhoneNumber { get; set; }

        public string AppVersion { get; set; }
    }
}
