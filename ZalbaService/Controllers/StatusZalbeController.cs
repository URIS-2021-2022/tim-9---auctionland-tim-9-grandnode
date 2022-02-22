using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Models;
using ZalbaService.Models.StatusZalbe;
using ZalbaService.ServiceCalls;

namespace ZalbaService.Controllers
{
    [Route("api/statuszalbe")]
    [ApiController]
    [Authorize]
    public class StatusZalbeController : ControllerBase
    {
        private readonly IStatusZalbeRepository statusZalbeRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "ZalbaService";
        private readonly Message message = new Message();


        public StatusZalbeController(IStatusZalbeRepository statusZalbeRepository, IMapper mapper, ILoggerService loggerService)
        {
            this.statusZalbeRepository = statusZalbeRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraća sve statuse zalbi.
        /// </summary>
        /// <returns>Lista statusa zalbi</returns>
        /// <response code="200">Vraća listu statusa zalbi</response>
        /// <response code="204">Nije pronađen ni jedan status zalbe u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<StatusZalbeDto>> GetAllStatusZalbe(string NazivStatusa)
        {
            var statusZalbe = statusZalbeRepository.GetAllStatusZalbe(NazivStatusa);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (statusZalbe == null || statusZalbe.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Radnja";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<StatusZalbeDto>>(statusZalbe));
        }
        /// <summary>
        /// Vraća status zalbe na osnovu identifikatora status zalbe.
        /// </summary>
        /// <param name="statusZalbeId">Identifikator status zalbe (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Status Zalbe</returns>
        /// <response code="200">Vraća status zalbe koji je pronađen</response>
        /// <response code="204">Ne postoji status zalbe sa datim identifikatorom</response>
        [HttpGet("{statusZalbeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StatusZalbeDto> GetStatusZalbe(Guid statusZalbeId)
        {
            var statusZalbe = statusZalbeRepository.GetStatusZalbeById(statusZalbeId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (statusZalbe == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of StatusZalbe with identifier: " + statusZalbeId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = statusZalbe.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<StatusZalbeDto>(statusZalbe));
        }
      

        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetStatusZalbeOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
