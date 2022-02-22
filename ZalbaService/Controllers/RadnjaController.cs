using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Models;
using ZalbaService.Models.Radnja;
using ZalbaService.ServiceCalls;

namespace ZalbaService.Controllers
{
    [Route("api/radnja")]
    [ApiController]
    [Authorize]
    public class RadnjaController : ControllerBase
    {

        private readonly IRadnjaRepository radnjaRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "ZalbaService";
        private readonly Message message = new Message();

        public RadnjaController(IRadnjaRepository radnjaRepository, IMapper mapper, ILoggerService loggerService)
        {
            this.radnjaRepository = radnjaRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }
        /// <summary>
        /// Vraća sve radnje na osnovu zalbe.
        /// </summary>
        /// <returns>Lista radnji na osnovu zalbe</returns>
        /// <response code="200">Vraća listu radnji na osnovu zalbe</response>
        /// <response code="204">Nije pronađena ni jedna radnja na osnovu zalbe u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<RadnjaDto>> GetAllRadnja(string NazivRadnje = null)
        {
            var radnja = radnjaRepository.GetAllRadnja(NazivRadnje);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (radnja == null || radnja.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Radnja";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<RadnjaDto>>(radnja));
        }
        /// <summary>
        /// Vraća radnju na osnovu identifikatora radnje na osnovu zalbe.
        /// </summary>
        /// <param name="radnjaId">Identifikator radnja (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Radnja na osnovu zalbe</returns>
        /// <response code="200">Vraća radnju na osnovu zalbe koja je pronađena</response>
        /// <response code="204">Ne postoji radnja na osnovu zalbe sa datim identifikatorom</response>
        [HttpGet("{radnjaId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<RadnjaDto> GetRadnja(Guid radnjaId)
        {
            var radnja =  radnjaRepository.GetRadnjaById(radnjaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (radnja == null)
            {

                message.Information = "Not found";
                message.Error = "There is no object of Radnja with identifier: " + radnjaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = radnja.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<RadnjaDto>(radnja));
        }

        
        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetRadnjaOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

    }
}
