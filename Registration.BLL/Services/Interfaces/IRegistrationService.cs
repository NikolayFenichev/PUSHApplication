using Registration.BLL.Dto;
using System;
using System.Threading.Tasks;

namespace Registration.BLL.Services.Interfaces
{
    public interface IRegistrationService: IDisposable
    {
        Task<MobileAppDto> RegistrationAsync(MobileAppDto mobileAppDto);
        Task<MobileAppDto> UnRegistrationAsync(string token);
    }
}
