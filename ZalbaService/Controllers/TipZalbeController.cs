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
using ZalbaService.Models.TipZalbe;
using ZalbaService.ServiceCalls;

namespace ZalbaService.Controllers
{
    [Route("api/tipzalbe")]
    [ApiController]
    //[Authorize]
    public class TipZalbeController : ControllerBase
    {
        private readonly ITipZalbeRepository tipZalbeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "ZalbaService";
        private Message message = new Message();

        public TipZalbeController(ITipZalbeRepository tipZalbeRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.tipZalbeRepository = tipZalbeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraća sve tipove zalbi.
        /// </summary>
        /// <returns>Lista tipova zalbi</returns>
        /// <response code="200">Vraća listu tipova zalbi</response>
        /// <response code="204">Nije pronađen ni jedan tip zalbe u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<TipZalbeDto>> GetAllTipZalbe(string NazivTipa)
        {
            var tipZalbe = tipZalbeRepository.GetAllTipZalbe(NazivTipa);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (tipZalbe == null || tipZalbe.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of TipZalbe";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<TipZalbeDto>>(tipZalbe));
        }
        /// <summary>
        /// Vraća tip zalbe na osnovu identifikatora tipa zalbe.
        /// </summary>
        /// <param name="tipZalbeId">Identifikator tip zalbe (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Tip Zalbe</returns>
        /// <response code="200">Vraća tip zalbe koji je pronađen</response>
        /// <response code="204">Ne postoji tip zalbe sa datim identifikatorom</response>
        [HttpGet("{tipZalbeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TipZalbeDto> GetTipZalbe(Guid tipZalbeId)
        {
            var tipZalbe =  tipZalbeRepository.GetTipZalbeById(tipZalbeId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (tipZalbe == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of TipZalbe with identifier: " + tipZalbeId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = tipZalbe.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<TipZalbeDto>(tipZalbe));
        }
        /*
        [HttpPost]
        public ActionResult<TipZalbeDto> CreateTipZalbe([FromBody] TipZalbeCreationDto tipZalbe)
        {
            try
            {
                TipZalbe tipz = mapper.Map<TipZalbe>(tipZalbe);
                tipz = tipZalbeRepository.CreateTipZalbe(tipz);
                tipZalbeRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetTipZalbe", "TipZalbe", new { tipZalbeId = tipz.TipZalbeId });
                return Created(lokacija, mapper.Map<TipZalbeDto>(tipz));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja tipa zalbe!");
            }
        }

        [HttpPut]
        public ActionResult<TipZalbeDto> UpdateTipZalbe(TipZalbeUpdateDto tipZalbe)
        {
            try
            {
                var stariTipZ = tipZalbeRepository.GetTipZalbeById(tipZalbe.TipZalbeId);
                if(stariTipZ == null)
                {
                    return NotFound();
                }
                TipZalbe noviTipZ = mapper.Map<TipZalbe>(tipZalbe);
                mapper.Map(noviTipZ, stariTipZ);
                tipZalbeRepository.SaveChanges();

                return Ok(mapper.Map<TipZalbeDto>(stariTipZ));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene tipa zalbe!");
            }
        }

        [HttpDelete("{tipZalbeId}")]
        public IActionResult DeleteTipZalbe(Guid tipZalbeId)
        {
            try
            {
                var tipZalbe =  tipZalbeRepository.GetTipZalbeById(tipZalbeId);
                if(tipZalbe == null)
                {
                    return NotFound();
                }
                tipZalbeRepository.DeleteTipZalbe(tipZalbeId);
                tipZalbeRepository.SaveChanges();

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja tipa zalbe!");
            }
        }*/

        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetTipZalbeOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }



    }
}
