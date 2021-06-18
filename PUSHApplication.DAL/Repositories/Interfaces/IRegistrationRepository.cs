using PUSHApplication.DAL.Models;
using System.Threading.Tasks;

namespace PUSHApplication.DAL.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<MobileApp> RegistrationAsync(MobileApp mobileApp);
        Task<MobileApp> UnRegistrationAsync(string token);
    }
}
