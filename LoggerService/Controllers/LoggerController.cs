using LoggerService.Data;
using LoggerService.Entities;
using Microsoft.AspNetCore.Mvc;
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

        public LoggerController(ILoggerRepository loggerRepository)
        {
            this.loggerRepository = loggerRepository;
        }

        [HttpPost]
        public ActionResult<Message> CreateMesage([FromBody] Message message)
        {

            message = loggerRepository.CreateMessage(message);
            return Created("", message); //Nema potrebe da vraca adresu logovima se ne pristupa
        }

    }
}
