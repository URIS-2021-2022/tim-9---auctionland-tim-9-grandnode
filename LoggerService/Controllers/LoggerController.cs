
using LoggerService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Controllers
{
    [ApiController]
    [Route("api/logger")]
    public class LoggerController : ControllerBase
    {
        private readonly ILogger<LoggerController> logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult<Message> CreateMesage([FromBody] Message message)
        {
            if (message.Error == null || message.Error == "")
            {
                logger.LogInformation("Service: " + message.ServiceName + ", Method: " + message.Method + ", Content: " + message.Information);
            }
            else 
            {
                logger.LogInformation("Service: " + message.ServiceName + ", Method: " + message.Method + ", Content: " + message.Information + ". Error: "
                    + message.Error);
            }
            return Created("", message); //Nema potrebe da vraca adresu logovima se ne pristupa
        }

    }
}
