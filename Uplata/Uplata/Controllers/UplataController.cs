using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Data;
using Uplata.Models;
using Uplata.ServiceCalls;

namespace Uplata.Controllers
{
    //Omogucava dodavanje dodatnih stvari kao sto su statusni kodovi
    [ApiController]
    [Route("api/uplate")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
    public class UplataController : ControllerBase
    {
        private readonly IUplataRepository uplataRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "UplataService";
        private Message message = new Message();

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public UplataController(IUplataRepository uplataRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.uplataRepository = uplataRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve uplate.
        /// </summary>
        /// <returns> Lista uplata</returns>
        /// <response code="200">Vraca listu uplata</response>
        /// <response code="404">Nije pronadjena ni jedna uplata</response>
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UplataDto>> GetUplate()
        {
            List<Entities.Uplata> uplate = uplataRepository.GetUplate();
            message.ServiceName = serviceName;
            message.Method = "GET";
            //ukoliko nije pronadjen ni jedna uplata vratiti status 204(NoContent)
            if (uplate == null || uplate.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Uplata";
            loggerService.CreateMessage(message);

            //ukoliko smo pronasli neku uplatu vratiti status 200 i listu uplata
            return Ok(mapper.Map<List<UplataDto>>(uplate));
        }

        /// <summary>
        /// Vraca jednu uplatu na osnovu ID-ja.
        /// </summary>
        /// // <param name="uplataID">ID uplate</param>
        /// <returns>Trazena uplata</returns>
        /// <response code="200">Vraca trazenu uplatu</response>
        /// <response code="404">Trazena uplata nije pronadjena</response>
        [HttpGet("{uplataID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UplataDto> GetUplata(Guid uplataID)
        {
            Entities.Uplata uplata = uplataRepository.GetUplataByID(uplataID);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (uplata == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Uplata with identifier: " + uplataID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = uplata.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<UplataDto>(uplata));
        }

        /// <summary>
        /// Kreira novu uplatu.
        /// </summary>
        /// /// <param name="uplataDto">Model uplate</param>
        /// <returns>Potvrdu o kreiranoj uplati.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje uplate \
        /// POST /api/uplate \
        /// {   \
        ///    "brojRacuna": "236541", \
        ///    "pozivNaBroj": "147852", \
        ///    "iznos": 9999, \
        ///    "svrhaUplate": "Uplata javnog nadmetanja", \
        ///    "datum": "2022-02-20T00:00:00", \
        ///    "kursnaListaID": "c8a3972c-ed80-4030-a6a3-61c37cc5b36d" \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiranu uplatu</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja uplate</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataConfirmationDto> CreateUplata([FromBody] UplataCreationDto uplataDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Entities.Uplata uplata = mapper.Map<Entities.Uplata>(uplataDto);
                UplataConfirmationDto u = uplataRepository.CreateUplata(uplata);
                uplataRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetUplata", "Uplata", new { uplataID = u.UplataID });
                message.Information = uplataDto.ToString() + " | Uplata location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<UplataConfirmationDto>(u));
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
        /// Azurira jednu uplatu.
        /// </summary>
        /// <param name="uplataDto">Model uplate koji se azurira</param>
        /// <returns>Potvrdu o modifikovanoj uplati</returns>
        /// <response code="200">Vraca azuriranu uplatu</response>
        /// <response code="400">Uplata koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja uplate</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UplataConfirmationDto> UpdateUplata(UplataUpdateDto uplataDto)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var starije = uplataRepository.GetUplataByID(uplataDto.UplataID);
                //provera da li postoji nadmetanje koje hocemo da azuriramo
                if (starije == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Uplata with identifier: " + uplataDto.UplataID;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                Entities.Uplata uplata = mapper.Map<Entities.Uplata>(uplataDto);
                //UplataConfirmationDto u = uplataRepository.UpdateUplata(uplata);
                mapper.Map(uplata, starije);
                uplataRepository.SaveChanges(); //Perzistiramo promene
                message.Information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<UplataConfirmationDto>(starije));
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
        /// Vrsi brisanje jedne uplate na osnovu ID-ja.
        /// </summary>
        /// <param name="uplataID">ID uplate</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uplata uspesno obrisana</response>
        /// <response code="404">Nije pronadjena uplata za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja uplate</response>
        [HttpDelete("{uplataID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUplata(Guid uplataID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                Entities.Uplata uplata = uplataRepository.GetUplataByID(uplataID);
                if (uplata == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Uplata with identifier: " + uplataID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                uplataRepository.DeleteUplata(uplataID);
                uplataRepository.SaveChanges(); //Perzistiramo promene
                message.Information = "Successfully deleted " + uplata.ToString();
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
        /// Vraca opcije dostupne za rad sa uplatama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetUplataOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
