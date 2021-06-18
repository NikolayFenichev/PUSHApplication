using MessageProcessing.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageProcessing.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageProcessingController : Controller
    {
        private IMessageProcessingService _messageProcessingService;

        public MessageProcessingController(IMessageProcessingService service)
        {
            _messageProcessingService = service;
        }

        [HttpGet]
        public void Start()
        {
            _messageProcessingService.Start();
        }
    }
}
