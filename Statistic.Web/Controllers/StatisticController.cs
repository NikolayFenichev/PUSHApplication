using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paging;
using Statistic.BLL.Models;
using Statistic.BLL.Services.Interfaces;
using Statistic.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statistic.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private IStatisticService _statisticService;
        private readonly ILogger<StatisticController> _logger;

        public StatisticController(IStatisticService service,
            ILogger<StatisticController> logger)
        {
            _statisticService = service;
            _logger = logger;
        }

        [HttpGet, Route("GetRegisteredVersions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRegisteredVersions()
        {
            try
            {
                var versionInfoDto = await _statisticService.GetRegisteredVersionsAsync();

                if (!versionInfoDto.Any())
                {
                    var message = "Версии не найдены";
                    _logger.LogError($"{nameof(GetRegisteredVersions)}: {message}");

                    return NotFound(message);
                }

                var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<VersionInfoDto, VersionInfoViewModel>()));
                var versionsInfoVm = mapper.Map<List<VersionInfoViewModel>>(versionInfoDto);

                return Ok(versionsInfoVm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetRegisteredVersions)}: {ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }

        [HttpGet, Route("GetMessagesByPhoneNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessagesByPhoneNumber([FromQuery] PageParameters pageParameters, string phoneNumber)
        {
            try
            {
                var messages = await _statisticService.GetMessagesByPhoneNumberAsync(pageParameters, phoneNumber);

                if (!messages.Any())
                {
                    var message = "Сообщения не найдены";
                    _logger.LogError($"{nameof(GetMessagesByPhoneNumber)}: {message}");

                    return NotFound(message);
                }

                var messagesPageInfo = new
                {
                    messages.TotalCount,
                    messages.PageSize,
                    messages.CurrentPage,
                    messages.TotalPages,
                    messages.HasNext,
                    messages.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(messagesPageInfo));

                return Ok(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetMessagesByPhoneNumber)}: {ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }
    }
}
