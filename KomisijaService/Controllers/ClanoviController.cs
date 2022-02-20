using AutoMapper;
using KomisijaService.Data.Interfaces;
using KomisijaService.Entities;
using KomisijaService.Models;
using KomisijaService.Models.Clanovi;
using KomisijaService.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Controllers
{
    [Route("api/clanovi")]
    [ApiController]
    //[Authorize]
    public class ClanoviController : ControllerBase
    {
        private readonly IClanoviRepository clanoviRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "KomisijaService";
        private Message message = new Message();
        private readonly ILicnostService licnostService;

        public ClanoviController(IClanoviRepository clanoviRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, ILicnostService licnostService)
        {
            this.clanoviRepository = clanoviRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.licnostService = licnostService;
        }
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ClanoviDto>> GetAllClanovi(Guid? komisijaId)
        {
            var clanovi = clanoviRepository.GetAllClanovi(komisijaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (clanovi == null || clanovi.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Clanovi";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<ClanoviDto>>(clanovi));
        }
        [HttpGet("{clanoviId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ClanoviDto> GetClanovi(Guid clanoviId)
        {
            var clanovi = clanoviRepository.GetClanoviById(clanoviId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (clanovi == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Clan with identifier: " + clanoviId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = clanovi.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ClanoviDto>(clanovi));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClanoviConfirmationDto> CreateClanovi([FromBody] ClanoviCreationDto clanovi)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";

            try
            {
                Clanovi _clanovi = mapper.Map<Clanovi>(clanovi);
                ClanoviConfirmationDto confirmation = clanoviRepository.CreateClanovi(_clanovi);
                clanoviRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetClanovi", "Clanovi", new { clanoviId = confirmation.ClanoviId });
                message.Information = clanovi.ToString() + " | Clan location: " + lokacija;
                loggerService.CreateMessage(message);
                var licnostInfo = mapper.Map<LicnostDto>(clanovi);
                licnostInfo.LicnostId = confirmation.ClanoviId;
                bool _licnost = licnostService.LicnostKomisije(licnostInfo);
                return Created(lokacija, mapper.Map<ClanoviConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja clana!");
            }
        }
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClanoviConfirmationDto> UpdateClanovi(ClanoviUpdateDto clanovi)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";

            try
            {
                var stariClanovi = clanoviRepository.GetClanoviById(clanovi.ClanoviId);
                if (stariClanovi == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Clan with identifier: " + clanovi.ClanoviId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Clanovi noviClanovi = mapper.Map<Clanovi>(clanovi);
                mapper.Map(noviClanovi, stariClanovi);
                clanoviRepository.SaveChanges();
                message.Information = stariClanovi.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ClanoviConfirmationDto>(stariClanovi));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene clana!");
            }
        }
        [HttpDelete("{clanoviId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteClanovi(Guid clanoviId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                var clanovi = clanoviRepository.GetClanoviById(clanoviId);
                if (clanovi == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Clan with identifier: " + clanoviId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                clanoviRepository.DeleteClanovi(clanoviId);
                clanoviRepository.SaveChanges();
                message.Information = "Successfully deleted " + clanovi.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + clanovi.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja clana!");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetClanoviOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
