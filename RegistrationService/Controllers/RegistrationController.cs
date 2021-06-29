using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registration.BLL.Dto;
using Registration.BLL.Services.Interfaces;
using RegistrationService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistrationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegistrationService _registrationService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistrationService service,
            ILogger<RegistrationController> logger)
        {
            _registrationService = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registration([FromBody] MobileAppViewModel mobileAppViewModel)
        {
            if (!ModelState.IsValid)
            {
                var message = "Неверная модель в параметре";
                _logger.LogError($"{nameof(Registration)}:{mobileAppViewModel} - {message}");

                return BadRequest(ModelState);
            }

            try
            {
                var mapper = new Mapper(new MapperConfiguration(cfg =>
                        cfg.CreateMap<MobileAppViewModel, MobileAppDto>()));
                var mobileAppDto = mapper.Map<MobileAppDto>(mobileAppViewModel);

                mobileAppDto = await _registrationService.RegistrationAsync(mobileAppDto);

                mapper = new Mapper(new MapperConfiguration(cfg =>
                        cfg.CreateMap<MobileAppDto, MobileAppViewModel>()));
                mobileAppViewModel = mapper.Map<MobileAppViewModel>(mobileAppDto);

                var routeValue = new
                {
                    mobileAppViewModel.FirebaseToken,
                    mobileAppViewModel.PhoneNumber,
                    mobileAppViewModel.AppVersion
                };

                _logger.LogInformation($"{nameof(UnRegistration)}: Пользователь зарегистрирован");

                return CreatedAtRoute(routeValue, mobileAppViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Registration)}: {ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnRegistration(string token)
        {
            try
            {
                var mobileAppDto = await _registrationService.UnRegistrationAsync(token);
                if (mobileAppDto == null)
                {
                    return NotFound();
                }

                _logger.LogInformation($"{nameof(UnRegistration)}: Пользователь удалён");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UnRegistration)}: {ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }
    }
}
