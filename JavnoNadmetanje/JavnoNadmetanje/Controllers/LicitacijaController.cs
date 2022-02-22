using AutoMapper;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using JavnoNadmetanje.ServiceCalls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Controllers
{
    //Omogucava dodavanje dodatnih stvari kao sto su statusni kodovi
    [ApiController]
    [Route("api/licitacije")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "JavnoNadmetanjeService";
        private readonly Message message = new Message();

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public LicitacijaController(ILicitacijaRepository licitacijaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve licitacije.
        /// </summary>
        /// <returns> Lista licitacija</returns>
        /// <response code="200">Vraca listu licitacija</response>
        /// <response code="404">Nije pronadjena ni jedna licitacija</response>
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LicitacijaDto>> GetLicitacije()
        {
            List<Licitacija> licitacije = licitacijaRepository.GetLicitacije();
            message.ServiceName = serviceName;
            message.Method = "GET";
            //ukoliko nije pronadjena ni jedna licitacija vratiti status 204(NoContent)
            if (licitacije == null || licitacije.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of Licitacija";
            loggerService.CreateMessage(message);
            //ukoliko smo pronasli neku licitaciju vratiti status 200 i listu pronadjenih licitacija
            return Ok(mapper.Map<List<LicitacijaDto>>(licitacije));
        }

        /// <summary>
        /// Vraca jednu licitaciju na osnovu ID-ja.
        /// </summary>
        /// // <param name="licitacijaID">ID licitacije</param>
        /// <returns>Trazena licitacija</returns>
        /// <response code="200">Vraca trazenu licitaciju</response>
        /// <response code="404">Trazena licitacija nije pronadjena</response>
        [HttpGet("{licitacijaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicitacijaDto> GetLicitacija(Guid licitacijaID)
        {
            Licitacija licitacija = licitacijaRepository.GetLicitacijaByID(licitacijaID);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (licitacija == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Licitacija with identifier: " + licitacijaID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = licitacija.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<LicitacijaDto>(licitacija));
        }

        /// <summary>
        /// Kreira novu licitaciju.
        /// </summary>
        /// /// <param name="licitacijaDto">Model licitacije</param>
        /// <returns>Potvrdu o kreiranoj licitaciji.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove licitacije \
        /// POST /api/licitacije \
        /// {   \
        ///    "broj": 3, \
        ///    "godina": 2022, \
        ///    "datum": "2022-02-19T00:00:00", \
        ///    "ogranicenja": 1, \
        ///    "korakCene": 300, \
        ///    "javnoNadmetanjeID": "208a48a5-371c-4f9d-ac23-18bb176ff8f3", \
        ///    "rokPrijava": "2022-02-17T00:00:00" \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiranu licitaciju</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja licitacije</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicitacijaConfirmationDto> CreateLicitacija([FromBody] LicitacijaCreationDto licitacijaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Licitacija licitacija = mapper.Map<Licitacija>(licitacijaDto);
                LicitacijaConfirmationDto l = licitacijaRepository.CreateLicitacija(licitacija);
                licitacijaRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetLicitacija", "Licitacija", new { licitacijaID = l.LicitacijaID });
                message.Information = licitacijaDto.ToString() + " | Licitacija location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<LicitacijaConfirmationDto>(l));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Azurira jednu licitaciju.
        /// </summary>
        /// <param name="licitacijaDto">Model licitacije koji se azurira</param>
        /// <returns>Potvrdu o modifikovanoj licitaciji</returns>
        /// <response code="200">Vraca azuriranu licitaciju</response>
        /// <response code="400">Licitacija koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja licitacije</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicitacijaConfirmationDto> UpdateLicitacija(LicitacijaUpdateDto licitacijaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var starije = licitacijaRepository.GetLicitacijaByID(licitacijaDto.LicitacijaID);
                //provera da li postoji licitacija koju hocemo da azuriramo
                if (starije == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Licitacija with identifier: " + licitacijaDto.LicitacijaID;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                Licitacija licitacija = mapper.Map<Licitacija>(licitacijaDto);
                mapper.Map(licitacija, starije);
                licitacijaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<LicitacijaConfirmationDto>(starije));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne licitacije na osnovu ID-ja.
        /// </summary>
        /// <param name="licitacijaID">ID licitacije</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Licitacija uspesno obrisana</response>
        /// <response code="404">Nije pronadjena licitacija za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja licitacije</response>
        [HttpDelete("{licitacijaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLicitacija(Guid licitacijaID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                Licitacija licitacija = licitacijaRepository.GetLicitacijaByID(licitacijaID);
                if (licitacija == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Licitacija with identifier: " + licitacijaID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                licitacijaRepository.DeleteLicitacija(licitacijaID);
                licitacijaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = "Successfully deleted " + licitacija.ToString();
                //Status kod tipa 2xx koji se koristi kada se ne vraca nikakav sadrzaj, ali naglasava da je sve uredu
                return NoContent();
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vraca opcije dostupne za rad sa licitacijama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetLicitacijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
