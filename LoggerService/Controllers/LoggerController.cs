using LoggerService.Data;
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
        private readonly ILoggerRepository loggerRepository;
        private readonly ILogger<LoggerController> logger;

        public LoggerController(ILoggerRepository loggerRepository, ILogger<LoggerController> logger)
        {
            this.loggerRepository = loggerRepository;
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult<Message> CreateMesage([FromBody] Message message)
        {

            message = loggerRepository.CreateMessage(message);
            logger.LogInformation("Sucessfully logged " + message.Information);
            return Created("", message); //Nema potrebe da vraca adresu logovima se ne pristupa
        }

    }
}
