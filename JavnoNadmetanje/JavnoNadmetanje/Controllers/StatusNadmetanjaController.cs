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
    [Route("api/statusiNadmetanja")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
    public class StatusNadmetanjaController : ControllerBase
    {
        private readonly IStatusNadmetanjaRepository statusNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "JavnoNadmetanjeService";
        private Message message = new Message();

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public StatusNadmetanjaController(IStatusNadmetanjaRepository statusNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.statusNadmetanjaRepository = statusNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve statuse javnih nadmetanja.
        /// </summary>
        /// <returns> Lista statusa javnih nadmetanja</returns>
        /// <response code="200">Vraca listu statusa javnih nadmetanja</response>
        /// <response code="404">Nije pronadjen ni jedan status javnog nadmetanja</response>
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StatusNadmetanjaDto>> GetStatusiNadmetanja()
        {
            List<StatusNadmetanja> statusiNadmetanja = statusNadmetanjaRepository.GetStatusiNadmetanja();
            message.ServiceName = serviceName;
            message.Method = "GET";
            //ukoliko nije pronadjen ni jedan status vratiti status 204(NoContent)
            if (statusiNadmetanja == null || statusiNadmetanja.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of StatusNadmetanja";
            loggerService.CreateMessage(message);
            //ukoliko smo pronasli neki status vratiti status 200 i listu pronadjenih statusa
            return Ok(mapper.Map<List<StatusNadmetanjaDto>>(statusiNadmetanja));
        }

        /// <summary>
        /// Vraca jedan status javnog nadmetanja na osnovu ID-ja.
        /// </summary>
        /// // <param name="statusNadmetanjaID">ID statusa javnog nadmetanja</param>
        /// <returns>Trazeni status nadmetanja</returns>
        /// <response code="200">Vraca trazeni status nadmetanje</response>
        /// <response code="404">Trazeni status nije pronadjen</response>
        [HttpGet("{statusNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StatusNadmetanjaDto> GetStatusNadmetanja(Guid statusNadmetanjaID)
        {
            StatusNadmetanja statusNadmetanja = statusNadmetanjaRepository.GetStatusNadmetanjaByID(statusNadmetanjaID);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (statusNadmetanja == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of StatusNadmetanja with identifier: " + statusNadmetanjaID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = statusNadmetanja.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<StatusNadmetanjaDto>(statusNadmetanja));
        }

        /// <summary>
        /// Kreira novi status nadmetanja.
        /// </summary>
        /// /// <param name="statusNadmetanjaDto">Model statusa nadmetanja</param>
        /// <returns>Potvrdu o kreiranom statusu nadmetanja.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog statusa nadmetanja \
        /// POST /api/statusiNadmetanja \
        /// {   \
        ///    "nazivStatusaNadmetanja": "Novi status" \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiran status nadmetanja</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja statusa nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusNadmetanjaConfirmationDto> CreateStatusNadmetanja([FromBody] StatusNadmetanjaCreationDto statusNadmetanjaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                StatusNadmetanja status = mapper.Map<StatusNadmetanja>(statusNadmetanjaDto);
                StatusNadmetanjaConfirmationDto s = statusNadmetanjaRepository.CreateStatusNadmetanja(status);
                statusNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetStatusNadmetanja", "StatusNadmetanja", new { statusNadmetanjaID = s.StatusNadmetanjaID });
                message.Information = statusNadmetanjaDto.ToString() + " | StatusNadmetanja location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<StatusNadmetanjaConfirmationDto>(s));
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
        /// Azurira jedan status nadmetanja.
        /// </summary>
        /// <param name="statusNadmetanjaDto">Model statusa nadmetanja koji se azurira</param>
        /// <returns>Potvrdu o modifikovanom statusu nadmetanja</returns>
        /// <response code="200">Vraca azuriran status nadmetanja</response>
        /// <response code="400">Status nadmetanja koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja statusa nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusNadmetanjaConfirmationDto> UpdateStatusNadmetanja(StatusNadmetanjaUpdateDto statusNadmetanjaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var starije = statusNadmetanjaRepository.GetStatusNadmetanjaByID(statusNadmetanjaDto.StatusNadmetanjaID);
                //provera da li postoji status koji hocemo da azuriramo
                if (starije == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of StatusNadmetanja with identifier: " + statusNadmetanjaDto.StatusNadmetanjaID;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                StatusNadmetanja statusNadmetanja = mapper.Map<StatusNadmetanja>(statusNadmetanjaDto);
                //StatusNadmetanjaConfirmationDto s = statusNadmetanjaRepository.UpdateStatusNadmetanja(statusNadmetanja);
                mapper.Map(statusNadmetanja, starije);
                statusNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<StatusNadmetanjaConfirmationDto>(starije));
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
        /// Vrsi brisanje jednog statusa javnog nadmetanja na osnovu ID-ja.
        /// </summary>
        /// <param name="statusNadmetanjaID">ID statusa nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Status nadmetanja uspesno obrisan</response>
        /// <response code="404">Nije pronadjen status nadmetanja za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja statusa nadmetanja</response>
        [HttpDelete("{statusNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStatusNadmetanja(Guid statusNadmetanjaID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                StatusNadmetanja statusNadmetanja = statusNadmetanjaRepository.GetStatusNadmetanjaByID(statusNadmetanjaID);
                if (statusNadmetanja == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of StatusNadmetanja with identifier: " + statusNadmetanjaID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                statusNadmetanjaRepository.DeleteStatusNadmetanja(statusNadmetanjaID);
                statusNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = "Successfully deleted " + statusNadmetanja.ToString();
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
        /// Vraca opcije dostupne za rad sa statusima nadmetanja.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetStatusNadmetanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
