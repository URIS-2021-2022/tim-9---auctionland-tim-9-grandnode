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
        /// <summary>
        /// Vraća sve clanove komisije.
        /// </summary>
        /// <returns>Lista clanova komisije</returns>
        /// <response code="200">Vraća listu clanova</response>
        /// <response code="204">Nije pronađen ni jedan clan u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ClanoviDto>> GetAllClanovi(Guid? komisijaId)
        {
            //var clanovi = clanoviRepository.GetAllClanovi(komisijaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            List<Clanovi> clanovi = clanoviRepository.GetAllClanovi();
            if (clanovi == null || clanovi.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            List<ClanoviDto> clanoviDto = mapper.Map<List<ClanoviDto>>(clanovi);

            foreach (ClanoviDto p in clanoviDto)
            {
                
                p.Licnost = licnostService.LicnostKomisije(p.ClanoviId).Result;
                
            }
            message.Information = "Returned list of Clanovi";
            loggerService.CreateMessage(message);
            return Ok(clanoviDto);
            // return Ok(mapper.Map<List<ClanoviDto>>(clanovi));
        }
        /// <summary>
        /// Vraća clana na osnovu identifikatora clan.
        /// </summary>
        /// <param name="clanId">Identifikator clana (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Clan</returns>
        /// <response code="200">Vraća clana koji je pronađen</response>
        /// <response code="204">Ne postoji clan sa datim identifikatorom</response>
        [HttpGet("{clanoviId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ClanoviDto> GetClanovi(Guid clanoviId)
        {
            Clanovi clanovi = clanoviRepository.GetClanoviById(clanoviId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (clanovi == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Clan with identifier: " + clanoviId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            ClanoviDto clanoviDto = mapper.Map<ClanoviDto>(clanovi);
            clanoviDto.Licnost = licnostService.LicnostKomisije(clanovi.ClanoviId).Result;
            message.Information = clanovi.ToString();
            loggerService.CreateMessage(message);
            return Ok(clanoviDto);
            //return Ok(mapper.Map<ClanoviDto>(clanovi));
        }
        /// <summary>
        /// Upisuje clana.
        /// </summary>
        /// <param name="clanDto">Model clana</param>
        /// <returns>Podatke o clanu koji je upisan</returns>
        /// <remarks>
        /// Primer zahteva za upis clana \
        /// POST /api/clan \
        /// {
        ///     "ClanId": "7684d0d5-2055-4a10-f724-08d9f3dcf86e",
        ///     "KomisijaId": "54a107-684d0d5-205-f724-08d9f3dcf86e"
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Vraća podatke o upisanom clanu</response>
        /// <response code="500">Postoji neki problem sa upisom</response>
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
        /// <summary>
        /// Menja vrednosti obeležja clan.
        /// </summary>
        /// <param name="clanDto">Model clana</param>
        /// <returns>Podatke o clanu koji je upisan</returns>
        ///     /// <remarks>
        /// Primer zahteva za upis clana \
        /// POST /api/clan \
        /// {
        ///     "ClanId": "8d6ab9eb-05d4-4010-6741-08d9f3bac53c",
        ///     "KomisijaId": "5679b9eb-05d4-4010-6741-08d9f3bac53c"
        /// }
        /// </remarks>
        /// <response code="200">Vraća podatke o izmenjenom clanu</response>
        /// <response code="404">Ne postoji clan za koji je pokušana izmena</response>
        /// <response code="500">Postoji neki problem sa izmenom</response>
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
        /// <summary>
        /// Briše clana na osnovu identifikatora.
        /// </summary>
        /// <param name="clanId">Identifikator clana (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>string</returns>
        /// <response code="204">Vraća poruku o uspešnom brisanju</response>
        /// <response code="404">Ne postoji clan sa tim identifikatorom</response>
        /// <response code="500">Postoji problem sa brisanjem na serveru</response>
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
        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetClanoviOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
