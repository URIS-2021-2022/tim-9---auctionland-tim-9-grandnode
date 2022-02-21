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
using ZalbaService.Models.Zalba;
using ZalbaService.ServiceCalls;

namespace ZalbaService.Controllers
{
    [Route("api/zalba")]
    [ApiController]
    [Authorize]
    public class ZalbaController : ControllerBase
    {
        private readonly IZalbaRepository zalbaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "ZalbaService";
        private Message message = new Message();
        private readonly IKupacService kupacService;

        public ZalbaController(IZalbaRepository zalbaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKupacService kupacService)
        {
            this.zalbaRepository = zalbaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.kupacService = kupacService;
        }
        /// <summary>
        /// Vraća sve zalbe.
        /// </summary>
        /// <returns>Lista zalbi</returns>
        /// <response code="200">Vraća listu zalbi</response>
        /// <response code="204">Nije pronađena ni jedna zalba u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ZalbaDto>> GetAllZalba()
        {
            
            message.ServiceName = serviceName;
            message.Method = "GET";
            List<Zalba> zalba = zalbaRepository.GetAllZalba();
            if (zalba == null || zalba.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            List<ZalbaDto> zalbaDto = mapper.Map<List<ZalbaDto>>(zalba);

            foreach (ZalbaDto p in zalbaDto)
            {
                p.Kupac = kupacService.GetPodnosiocaZalbe(p.PodnosilacZalbe).Result;
            }

            message.Information = "Returned list of Zalba";
            loggerService.CreateMessage(message);
            return Ok(zalbaDto);
            //return Ok(mapper.Map<List<ZalbaDto>>(zalba));
        }

        /// <summary>
        /// Vraća zalbu na osnovu identifikatora zalba.
        /// </summary>
        /// <param name="zalbaId">Identifikator zalba (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Zalba</returns>
        /// <response code="200">Vraća zalbu koji je pronađen</response>
        /// <response code="204">Ne postoji zalba sa datim identifikatorom</response>
        [HttpGet("{zalbaId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ZalbaDto> GetZalba(Guid zalbaId)
        {
            Zalba zalba = zalbaRepository.GetZalbaById(zalbaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (zalba == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Zalba with identifier: " + zalbaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            ZalbaDto zalbaDto = mapper.Map<ZalbaDto>(zalba);
            zalbaDto.Kupac = kupacService.GetPodnosiocaZalbe(zalba.PodnosilacZalbe).Result;
            message.Information = zalba.ToString();
            loggerService.CreateMessage(message);
            return Ok(zalbaDto);
            //return Ok(mapper.Map<ZalbaDto>(zalba));
        }

        /// <summary>
        /// Upisuje zalbu.
        /// </summary>
        /// <param name="zalbaDto">Model zalbe</param>
        /// <returns>Podatke o zalbi koja je upisana</returns>
        /// <remarks>
        /// Primer zahteva za upis zalbe \
        /// POST /api/zalba \
        /// {
        ///     "ZalbaId": "7684d0d5-2055-4a10-f724-08d9f3dcf86e",
        ///     "TipZalbeId" : "1584d0d5-2055-4a10-f724-08d9f3dcf72m",
        ///     "DatumZalbe" : "2021-04-20T11:00:00",
        ///     "PodnosilacZalbe" : "bb14ca98-fcc0-4063-8a2b-341c3f38cdc4" ,
        ///     "Razlog" : "Krsenje pravilnika za javno nadmetanje",
        ///     "Obrazlozenje" : "Neispravnost prilikom dodeljivanja parcele",
        ///     "DatumResenja" : "2021-06-03T10:00:00",
        ///     "BrojResenja" : "15487",
        ///     "StatusZalbeId" : "212b6e83-ab50-49ec-bd95-92cd5e8f8a25",
        ///     "BrojOdluke" : "12540",
        ///     "RadnjaId" : "3eeede02-9e9e-46d2-8034-d21125e45b43"
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Vraća podatke o upisanoj zalbi</response>
        /// <response code="500">Postoji neki problem sa upisom</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZalbaConfirmationDto> CreateZalba([FromBody] ZalbaCreationDto zalba)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Zalba _zalba = mapper.Map<Zalba>(zalba);
                ZalbaConfirmationDto confirmation = zalbaRepository.CreateZalba(_zalba);
                zalbaRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetZalba", "Zalba", new { zalbaId = confirmation.ZalbaId });
                message.Information = zalba.ToString() + " | Zalba location: " + lokacija;
                loggerService.CreateMessage(message);
                
                return Created(lokacija, mapper.Map<ZalbaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja zalbe!");
            }
        }

        /// <summary>
        /// Menja vrednosti obeležja zalba.
        /// </summary>
        /// <param name="zalbaDto">Model zalbe</param>
        /// <returns>Podatke o zalbi koja je upisana</returns>
        ///     /// <remarks>
        /// Primer zahteva za upis zalbe \
        /// POST /api/zalba \
        /// {
        ///      "ZalbaId": "7684d0d5-2055-4a10-f724-08d9f3dcf86e",
        ///     "TipZalbeId" : "1584d0d5-2055-4a10-f724-08d9f3dcf72m",
        ///     "DatumZalbe" : "2021-04-20T11:00:00",
        ///     "PodnosilacZalbe" : "bb14ca98-fcc0-4063-8a2b-341c3f38cdc4" ,
        ///     "Razlog" : "Krsenje pravilnika za javno nadmetanje",
        ///     "Obrazlozenje" : "Neispravnost prilikom dodeljivanja parcele",
        ///     "DatumResenja" : "2021-06-03T10:00:00",
        ///     "BrojResenja" : "15487",
        ///     "StatusZalbeId" : "212b6e83-ab50-49ec-bd95-92cd5e8f8a25",
        ///     "BrojOdluke" : "12540",
        ///     "RadnjaId" : "3eeede02-9e9e-46d2-8034-d21125e45b43"
        /// }
        /// </remarks>
        /// <response code="200">Vraća podatke o izmenjenoj zalbi</response>
        /// <response code="404">Ne postoji zalba za koju je pokušana izmena</response>
        /// <response code="500">Postoji neki problem sa izmenom</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZalbaConfirmationDto> UpdateZalba(ZalbaUpdateDto zalba)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var staraZalba = zalbaRepository.GetZalbaById(zalba.ZalbaId);
                if (staraZalba == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Zalba with identifier: " + zalba.ZalbaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Zalba novaZalba = mapper.Map<Zalba>(zalba);
                mapper.Map(novaZalba, staraZalba);
                zalbaRepository.SaveChanges();
                message.Information = staraZalba.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ZalbaConfirmationDto>(staraZalba));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene zalbe!");
            }
        }

        /// <summary>
        /// Briše zalbu na osnovu identifikatora.
        /// </summary>
        /// <param name="zalbaId">Identifikator zalbe (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>string</returns>
        /// <response code="204">Vraća poruku o uspešnom brisanju</response>
        /// <response code="404">Ne postoji zalba sa tim identifikatorom</response>
        /// <response code="500">Postoji problem sa brisanjem na serveru</response>
        [HttpDelete("{zalbaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteZalba(Guid zalbaId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                var zalba = zalbaRepository.GetZalbaById(zalbaId);
                if (zalba == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Zalba with identifier: " + zalbaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                zalbaRepository.DeleteZalba(zalbaId);
                zalbaRepository.SaveChanges();
                message.Information = "Successfully deleted " + zalba.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + zalba.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja zalbe!");
            }
        }
        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetZalbaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
