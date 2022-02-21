using AutoMapper;
using KomisijaService.Data.Interfaces;
using KomisijaService.Entities;
using KomisijaService.Models;
using KomisijaService.Models.Komisija;
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
    [Route("api/komisija")]
    [ApiController]
    [Authorize]
    public class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "KomisijaService";
        private Message message = new Message();

        public KomisijaController(IKomisijaRepository komisijaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.komisijaRepository = komisijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }
        /// <summary>
        /// Vraća sve komisije.
        /// </summary>
        /// <returns>Lista komisija</returns>
        /// <response code="200">Vraća listu komisija</response>
        /// <response code="204">Nije pronađena ni jedna komisija u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<KomisijaDto>> GetAllKomisija(Guid? predsednikId)
        {
            var komisija = komisijaRepository.GetAllKomisija(predsednikId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (komisija == null || komisija.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Komisija";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<KomisijaDto>>(komisija));
        }
        /// <summary>
        /// Vraća komisiju na osnovu identifikatora komisija.
        /// </summary>
        /// <param name="komisijaId">Identifikator komisije (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Clan</returns>
        /// <response code="200">Vraća komisiju koja je pronađena</response>
        /// <response code="204">Ne postoji komisija sa datim identifikatorom</response>
        [HttpGet("{komisijaId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KomisijaDto> GetKomisija(Guid komisijaId)
        {
            var komisija = komisijaRepository.GetKomisijaById(komisijaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (komisija == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Komisija with identifier: " + komisijaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = komisija.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<KomisijaDto>(komisija));
        }
        /// <summary>
        /// Upisuje komisiju.
        /// </summary>
        /// <param name="komisijaDto">Model komisije</param>
        /// <returns>Podatke o komisiji koja je upisana</returns>
        /// <remarks>
        /// Primer zahteva za upis komisije \
        /// POST /api/komisija \
        /// {
        ///     "KomisijaId": "7684d0d5-2055-4a10-f724-08d9f3dcf86e",
        ///     "PredsednikId": "54a107-684d0d5-205-f724-08d9f3dcf86e"
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Vraća podatke o upisanoj komisiji</response>
        /// <response code="500">Postoji neki problem sa upisom</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KomisijaConfirmationDto> CreateKomisija([FromBody] KomisijaCreationDto komisija)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Komisija _komisija = mapper.Map<Komisija>(komisija);
                KomisijaConfirmationDto confirmation = komisijaRepository.CreateKomisija(_komisija);
                komisijaRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetKomisija", "Komisija", new { komisijaId = confirmation.KomisijaId });
                message.Information = komisija.ToString() + " | Komisija location: " + lokacija;
                loggerService.CreateMessage(message);
                return Created(lokacija, mapper.Map<KomisijaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja komisije!");
            }
        }
        /// <summary>
        /// Menja vrednosti obeležja komisija.
        /// </summary>
        /// <param name="komisijaDto">Model komisije</param>
        /// <returns>Podatke o komisiji koja je upisana</returns>
        ///     /// <remarks>
        /// Primer zahteva za upis komisije \
        /// POST /api/clan \
        /// {
        ///     "KomisijaId": "8d6ab9eb-05d4-4010-6741-08d9f3bac53c",
        ///     "PredsednikId": "5679b9eb-05d4-4010-6741-08d9f3bac53c"
        /// }
        /// </remarks>
        /// <response code="200">Vraća podatke o izmenjenoj komisiji</response>
        /// <response code="404">Ne postoji komisija za koju je pokušana izmena</response>
        /// <response code="500">Postoji neki problem sa izmenom</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KomisijaConfirmationDto> UpdateKomisija(KomisijaUpdateDto komisija)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var staraKomisija = komisijaRepository.GetKomisijaById(komisija.KomisijaId);
                if (staraKomisija == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Komisija with identifier: " + komisija.KomisijaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Komisija novaKomisija = mapper.Map<Komisija>(komisija);
                mapper.Map(novaKomisija, staraKomisija);
                komisijaRepository.SaveChanges();
                message.Information = staraKomisija.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<KomisijaConfirmationDto>(staraKomisija));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene komisije!");
            }
        }
        /// <summary>
        /// Briše komisiju na osnovu identifikatora.
        /// </summary>
        /// <param name="komisijaId">Identifikator komisije (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>string</returns>
        /// <response code="204">Vraća poruku o uspešnom brisanju</response>
        /// <response code="404">Ne postoji komisija sa tim identifikatorom</response>
        /// <response code="500">Postoji problem sa brisanjem na serveru</response>
        [HttpDelete("{komisijaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKomisija(Guid komisijaId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                var komisija = komisijaRepository.GetKomisijaById(komisijaId);
                if (komisija == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Komisija with identifier: " + komisijaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                komisijaRepository.DeleteKomisija(komisijaId);
                komisijaRepository.SaveChanges();
                message.Information = "Successfully deleted " + komisija.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + komisija.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja komisije!");
            }
        }
        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetKomisijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
