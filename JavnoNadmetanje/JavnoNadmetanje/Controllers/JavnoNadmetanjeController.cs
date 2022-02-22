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
    [Route("api/javnaNadmetanja")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
    public class JavnoNadmetanjeController : ControllerBase
    {
        private readonly IJavnoNadmetanjeRepository javnoNadmetanjeRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "JavnoNadmetanjeService";
        private readonly Message message = new Message();
        private readonly IKupacService kupacService;

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKupacService kupacService)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.kupacService = kupacService;
        }

        /// <summary>
        /// Vraca sva javna nadmetanja.
        /// </summary>
        /// <returns> Lista javnih nadmetanja</returns>
        /// <response code="200">Vraca listu javnih nadmetanja</response>
        /// <response code="404">Nije pronadjen ni jedno javno nadmetanje</response>
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnaNadmetanja()
        {
            List<Entities.JavnoNadmetanje> javnaNadmetanja = javnoNadmetanjeRepository.GetJavnaNadmetanja();
            message.ServiceName = serviceName;
            message.Method = "GET";
            //ukoliko nije pronadjen ni jedan tip vratiti status 204(NoContent)
            if (javnaNadmetanja == null || javnaNadmetanja.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            List<JavnoNadmetanjeDto> javnoNadmetanjeDto = mapper.Map<List<JavnoNadmetanjeDto>>(javnaNadmetanja);

            foreach (JavnoNadmetanjeDto j in javnoNadmetanjeDto)
            {
                j.Kupac = kupacService.GetNajboljegPonudjaca(j.KupacID).Result;
            }

            message.Information = "Returned list of JavnoNadmetanje";
            loggerService.CreateMessage(message);

            //ukoliko smo pronasli neko nadmetanje vratiti status 200 i listu pronadjenih nadmetanja
            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnoNadmetanjeDto));
        }

        /// <summary>
        /// Vraca jedno javno nadmetanje na osnovu ID-ja.
        /// </summary>
        /// // <param name="javnoNadmetanjeID">ID javnog nadmetanja</param>
        /// <returns>Trazeno javno nadmetanje</returns>
        /// <response code="200">Vraca trazeno javno nadmetanje</response>
        /// <response code="404">Trazeno javno nadmetanje nije pronadjeno</response>
        [HttpGet("{javnoNadmetanjeID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            Entities.JavnoNadmetanje javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeByID(javnoNadmetanjeID);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (javnoNadmetanje == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of JavnoNadmetanje with identifier: " + javnoNadmetanjeID;
                loggerService.CreateMessage(message);
                return NotFound();
            }

            JavnoNadmetanjeDto javnoNadmetanjeDto = mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje);
            javnoNadmetanjeDto.Kupac = kupacService.GetNajboljegPonudjaca(javnoNadmetanje.KupacID).Result;

            message.Information = javnoNadmetanje.ToString();
            loggerService.CreateMessage(message);
            return Ok(javnoNadmetanjeDto);
        }

        /// <summary>
        /// Kreira novo javno nadmetanje.
        /// </summary>
        /// /// <param name="javnoNadmetanjeDto">Model javnog nadmetanja</param>
        /// <returns>Potvrdu o kreiranom javnom nadmetanju.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog javnog nadmetanja \
        /// POST /api/javnaNadmetanja \
        /// {   \
        ///    "datum": "2022-02-15T00:00:00", \
        ///    "vremePocetka": "2022-02-15T07:00:00", \
        ///    "vremeKraja": "2022-02-15T10:00:00", \
        ///    "pocetnaCenaPoHektaru": 4000, \
        ///    "izuzeto": false, \
        ///    "tipJavnogNadmetanjaID": "4246a611-7b2f-429d-a9ba-0e539c81b82f", \
        ///    "izlicitiranaCena": 6000, \
        ///    "periodZakupa": 12, \
        ///    "brojUcesnika": 10, \
        ///    "visinaDopuneDepozita": 400, \
        ///    "krug": 1, \
        ///    "statusNadmetanjaID": "8aaa90c8-56f3-4a76-b07a-f895eded5a84", \
        ///    "adresaID": "a06f99d2-0ba7-40ff-a241-304a03dfe4be", \
        ///    "ovlascenoLiceID": "5cfa282f-8324-4a8b-8c23-8d43502ca01e", \
        ///    "kupacID": "8b3b7775-4293-4b41-9ccc-19f9cf694d68" \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreirano javno nadmetanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JavnoNadmetanjeConfirmationDto> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeCreationDto javnoNadmetanjeDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Entities.JavnoNadmetanje nadmetanje = mapper.Map<Entities.JavnoNadmetanje>(javnoNadmetanjeDto);
                JavnoNadmetanjeConfirmationDto j = javnoNadmetanjeRepository.CreateJavnoNadmetanje(nadmetanje);
                javnoNadmetanjeRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetJavnoNadmetanje", "JavnoNadmetanje", new { javnoNadmetanjeID = j.JavnoNadmetanjeID });
                message.Information = javnoNadmetanjeDto.ToString() + " | JavnoNadmetanje location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<JavnoNadmetanjeConfirmationDto>(j));
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
        /// Azurira jedno javno nadmetanje.
        /// </summary>
        /// <param name="javnoNadmetanjeDto">Model javnog nadmetanja koji se azurira</param>
        /// <returns>Potvrdu o modifikovanom javnom nadmetanju</returns>
        /// <response code="200">Vraca azurirano javno nadmetanje</response>
        /// <response code="400">Javno nadmetanje koje se azurira nije pronadjeno</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JavnoNadmetanjeConfirmationDto> UpdateJavnoNadmetanje(JavnoNadmetanjeUpdateDto javnoNadmetanjeDto)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var starije = javnoNadmetanjeRepository.GetJavnoNadmetanjeByID(javnoNadmetanjeDto.JavnoNadmetanjeID);
                //provera da li postoji nadmetanje koje hocemo da azuriramo
                if (starije == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of JavnoNadmetanje with identifier: " + javnoNadmetanjeDto.JavnoNadmetanjeID;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                Entities.JavnoNadmetanje javnoNadmetanje = mapper.Map<Entities.JavnoNadmetanje>(javnoNadmetanjeDto);
                mapper.Map(javnoNadmetanje, starije);
                javnoNadmetanjeRepository.SaveChanges(); //Perzistiramo promene
                message.Information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<JavnoNadmetanjeConfirmationDto>(starije));
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
        /// Vrsi brisanje jednog javnog nadmetanja na osnovu ID-ja.
        /// </summary>
        /// <param name="javnoNadmetanjeID">ID javnog nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Javno nadmetanje uspesno obrisano</response>
        /// <response code="404">Nije pronadjeno javno nadmetanje za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja javnog nadmetanja</response>
        [HttpDelete("{javnoNadmetanjeID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                Entities.JavnoNadmetanje javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeByID(javnoNadmetanjeID);
                if (javnoNadmetanje == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of JavnoNadmetanje with identifier: " + javnoNadmetanjeID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                javnoNadmetanjeRepository.DeleteJavnoNadmetanje(javnoNadmetanjeID);
                javnoNadmetanjeRepository.SaveChanges(); //Perzistiramo promene
                message.Information = "Successfully deleted " + javnoNadmetanje.ToString();
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
        /// Vraca opcije dostupne za rad sa javnim nadmetanjima.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetJavnoNadmetanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
