using AutoMapper;
using PUSHApplication.DAL.Models;
using PUSHApplication.DAL.Repositories.Interfaces;
using Registration.BLL.Dto;
using Registration.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace Registration.BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        IUnitOfWork _database;

        public RegistrationService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<MobileAppDto> RegistrationAsync(MobileAppDto mobileAppDto)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<MobileAppDto, MobileApp>()));
            var mobileApp = mapper.Map<MobileApp>(mobileAppDto);

            mobileApp = await _database.RegistrationRepository.RegistrationAsync(mobileApp);

            mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<MobileApp, MobileAppDto>()));
            mobileAppDto = mapper.Map<MobileAppDto>(mobileApp);

            return mobileAppDto;
        }

        public async Task<MobileAppDto> UnRegistrationAsync(string token)
        {
            var mobileApp = await _database.RegistrationRepository.UnRegistrationAsync(token);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<MobileApp, MobileAppDto>()));
            var mobileAppDto = mapper.Map<MobileAppDto>(mobileApp);

            return mobileAppDto;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
