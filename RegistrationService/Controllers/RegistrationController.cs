using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registration.BLL.Dto;
using Registration.BLL.Services.Interfaces;
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
        public async Task<IActionResult> Registration([FromBody] MobileAppDto mobileAppDto)
        {
            if (!ModelState.IsValid)
            {
                var message = "Неверная модель в параметре";
                _logger.LogError($"{nameof(Registration)}:{mobileAppDto} - {message}");

                return BadRequest(ModelState);
            }

            try
            {
                mobileAppDto = await _registrationService.RegistrationAsync(mobileAppDto);

                var routeValue = new
                {
                    mobileAppDto.FirebaseToken,
                    mobileAppDto.PhoneNumber,
                    mobileAppDto.AppVersion
                };

                return CreatedAtRoute(routeValue, mobileAppDto);
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
