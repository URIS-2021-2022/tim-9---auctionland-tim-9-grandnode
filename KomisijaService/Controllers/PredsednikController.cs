using AutoMapper;
using KomisijaService.Data.Interfaces;
using KomisijaService.Entities;
using KomisijaService.Models;
using KomisijaService.Models.Predsednik;
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
    [Route("api/predsednik")]
    [ApiController]
    [Authorize]
    public class PredsednikController : ControllerBase
    {
        private readonly IPredsednikRepository predsednikRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "KomisijaService";
        private Message message = new Message();
        private readonly ILicnostService licnostService;


        public PredsednikController(IPredsednikRepository predsednikRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, ILicnostService licnostService)
        {
            this.predsednikRepository = predsednikRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.licnostService = licnostService;
        }
        /// <summary>
        /// Vraća sve predsednike komisije.
        /// </summary>
        /// <returns>Lista predsednika komisije</returns>
        /// <response code="200">Vraća listu predsednika</response>
        /// <response code="204">Nije pronađen ni jedan predsednik u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PredsednikDto>> GetAllPredsednik()
        {
            //var predsednik = predsednikRepository.GetAllPredsednik();
            message.ServiceName = serviceName;
            message.Method = "GET";
            List<Predsednik> predsednik = predsednikRepository.GetAllPredsednik();
            if (predsednik == null || predsednik.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            List<PredsednikDto> predsednikDto = mapper.Map<List<PredsednikDto>>(predsednik);

            foreach (PredsednikDto p in predsednikDto)
            {
                p.Licnost = licnostService.LicnostKomisije(p.PredsednikId).Result;
            }

            message.Information = "Returned list of Predsednik";
            loggerService.CreateMessage(message);
            return Ok(predsednikDto);
            //return Ok(mapper.Map<List<PredsednikDto>>(predsednik));
        }
        /// <summary>
        /// Vraća predsednika na osnovu identifikatora predsednik.
        /// </summary>
        /// <param name="predsednikId">Identifikator predsednika (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Predsednik</returns>
        /// <response code="200">Vraća predsednika koji je pronađen</response>
        /// <response code="204">Ne postoji predsednik sa datim identifikatorom</response>
        [HttpGet("{predsednikId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PredsednikDto> GetPredsednik(Guid predsednikId)
        {
            Predsednik predsednik = predsednikRepository.GetPredsednikById(predsednikId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (predsednik == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Predsednik with identifier: " + predsednikId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            PredsednikDto predsednikDto = mapper.Map<PredsednikDto>(predsednik);
            predsednikDto.Licnost = licnostService.LicnostKomisije(predsednik.PredsednikId).Result;
            message.Information = predsednik.ToString();
            loggerService.CreateMessage(message);
            return Ok(predsednikDto);
            //return Ok(mapper.Map<PredsednikDto>(predsednik));
        }
        /// <summary>
        /// Upisuje predsednika.
        /// </summary>
        /// <param name="predsednikDto">Model predsednika</param>
        /// <returns>Podatke o predsedniku koji je upisan</returns>
        /// <remarks>
        /// Primer zahteva za upis predsednika \
        /// POST /api/predsednik \
        /// {
        ///     "PredsednikId": "7684d0d5-2055-4a10-f724-08d9f3dcf86e",
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Vraća podatke o upisanom predsedniku</response>
        /// <response code="500">Postoji neki problem sa upisom</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PredsednikConfirmationDto> CreatePredsednik([FromBody] PredsednikCreationDto predsednik)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Predsednik _predsednik = mapper.Map<Predsednik>(predsednik);
                PredsednikConfirmationDto confirmation = predsednikRepository.CreatePredsednik(_predsednik);
                predsednikRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetPredsednik", "Predsednik", new { predsednikId = confirmation.PredsednikId });
                message.Information = predsednik.ToString() + " | Predsednik location: " + lokacija;
                loggerService.CreateMessage(message);
          
                return Created(lokacija, mapper.Map<PredsednikConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja predsednika!");
            }
        }

        /// <summary>
        /// Briše predsednika na osnovu identifikatora.
        /// </summary>
        /// <param name="predsednikId">Identifikator predsednika (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>string</returns>
        /// <response code="204">Vraća poruku o uspešnom brisanju</response>
        /// <response code="404">Ne postoji predsednik sa tim identifikatorom</response>
        /// <response code="500">Postoji problem sa brisanjem na serveru</response>
        [HttpDelete("{predsednikId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePredsednik(Guid predsednikId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                var predsednik = predsednikRepository.GetPredsednikById(predsednikId);
                if (predsednik == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Predsednik with identifier: " + predsednikId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                predsednikRepository.DeletePredsednik(predsednikId);
                predsednikRepository.SaveChanges();
                message.Information = "Successfully deleted " + predsednik.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + predsednik.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja predsednika!");
            }
        }

        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPredsednikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, DELETE");
            return Ok();
        }
    }
}
