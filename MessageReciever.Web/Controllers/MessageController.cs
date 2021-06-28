using AutoMapper;
using MessageReciever.BLL.Models;
using MessageReciever.BLL.Services.Interfaces;
using MessageReciever.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageReciever.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private ISendMessageService _sendMessageService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(ISendMessageService service,
            ILogger<MessageController> logger)
        {
            _sendMessageService = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Send([FromBody] MessageViewModel messageVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapper = new Mapper(new MapperConfiguration(cfg =>
                        cfg.CreateMap<MessageViewModel, MessageDto>()));
                    var message = mapper.Map<MessageDto>(messageVm);

                    _sendMessageService.Send(message);

                    _logger.LogError($"{nameof(Send)}: Сообщение отправлено в сервис обработки");

                    return Ok();
                }

                var error = "Неверная модель в параметре";
                _logger.LogError($"{nameof(Send)}:{messageVm} - {error}");

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Send)}: {ex.Message} \n {ex.StackTrace}");
                throw;
            }

        }
    }
}
