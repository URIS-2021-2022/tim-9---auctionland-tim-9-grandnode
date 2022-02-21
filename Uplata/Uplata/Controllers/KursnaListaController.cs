using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Data;
using Uplata.Entities;
using Uplata.Models;
using Uplata.ServiceCalls;

namespace Uplata.Controllers
{
    //Omogucava dodavanje dodatnih stvari kao sto su statusni kodovi
    [ApiController]
    [Route("api/kursneListe")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
    public class KursnaListaController : ControllerBase
    {
        private readonly IKursnaListaRepository kursnaListaRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "UplataService";
        private Message message = new Message();

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public KursnaListaController(IKursnaListaRepository kursnaListaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.kursnaListaRepository = kursnaListaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve kursne liste.
        /// </summary>
        /// <returns> Lista kursnih lista</returns>
        /// <response code="200">Vraca listu kursnih lista</response>
        /// <response code="404">Nije pronadjena ni jedna kursna lista</response>
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<KursnaListaDto>> GetKursneListe()
        {
            List<KursnaLista> kursneListe = kursnaListaRepository.GetKursneListe();
            //ukoliko nije pronadjen ni jedna lista vratiti status 204(NoContent)
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (kursneListe == null || kursneListe.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of KursnaLista";
            loggerService.CreateMessage(message);
            //ukoliko smo pronasli neku listu vratiti status 200 i listu pronadjenih kursnih lista
            return Ok(mapper.Map<List<KursnaListaDto>>(kursneListe));
        }

        /// <summary>
        /// Vraca jednu kursnu listu na osnovu ID-ja.
        /// </summary>
        /// // <param name="kursnaListaID">ID kursne liste</param>
        /// <returns>Trazena kursna lista</returns>
        /// <response code="200">Vraca trazenu kursnu listu</response>
        /// <response code="404">Trazena kursna lista nije pronadjena</response>
        [HttpGet("{kursnaListaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<KursnaListaDto> GetKursnaLista(Guid kursnaListaID)
        {
            KursnaLista kursnaLista = kursnaListaRepository.GetKursnaListaByID(kursnaListaID);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (kursnaLista == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of KursnaLista with identifier: " + kursnaListaID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = kursnaLista.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<KursnaListaDto>(kursnaLista));
        }

        /// <summary>
        /// Kreira novu kursnu listu.
        /// </summary>
        /// /// <param name="kursnaListaDto">Model kursne liste</param>
        /// <returns>Potvrdu o kreiranoj kursnoj listi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove kursne liste \
        /// POST /api/kursneListe \
        /// {   \
        ///    "datum": "2022-02-10T00:00:00", \
        ///    "valuta": "RSD", \
        ///    "vrednost": 5555 \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiranu kursnu listu</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja kursne liste</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KursnaListaConfirmationDto> CreateKusrnaLista([FromBody] KursnaListaCreationDto kursnaListaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                KursnaLista lista = mapper.Map<KursnaLista>(kursnaListaDto);
                KursnaListaConfirmationDto k = kursnaListaRepository.CreateKursnaLista(lista);
                kursnaListaRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetKursnaLista", "KursnaLista", new { kursnaListaID = k.KursnaListaID });
                message.Information = kursnaListaDto.ToString() + " | KursnaLista location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<KursnaListaConfirmationDto>(k));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Azurira jednu kursnu listu.
        /// </summary>
        /// <param name="kursnaListaDto">Model kursneListe koji se azurira</param>
        /// <returns>Potvrdu o modifikovanoj kursnoj listi</returns>
        /// <response code="200">Vraca azuriranu kursnu listu</response>
        /// <response code="400">Kursna lista koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja kursne liste</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KursnaListaConfirmationDto> UpdateKursnaLista(KursnaListaUpdateDto kursnaListaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var starije = kursnaListaRepository.GetKursnaListaByID(kursnaListaDto.KursnaListaID);
                //provera da li postoji lista koju hocemo da azuriramo
                if (starije == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of KursnaLista with identifier: " + kursnaListaDto.KursnaListaID;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                KursnaLista kursnaLista = mapper.Map<KursnaLista>(kursnaListaDto);
                //KursnaListaConfirmationDto k = kursnaListaRepository.UpdateKursnaLista(kursnaLista);
                mapper.Map(kursnaLista, starije);
                kursnaListaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<KursnaListaConfirmationDto>(starije));
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
        /// Vrsi brisanje jedne kursne liste na osnovu ID-ja.
        /// </summary>
        /// <param name="kursnaListaID">ID kursne liste</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kursna lista uspesno obrisana</response>
        /// <response code="404">Nije pronadjena kursna lista za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja kursne liste</response>
        [HttpDelete("{kursnaListaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteKursnaLista(Guid kursnaListaID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                KursnaLista kursnaLista = kursnaListaRepository.GetKursnaListaByID(kursnaListaID);
                if (kursnaLista == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of KursnaLista with identifier: " + kursnaListaID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                kursnaListaRepository.DeleteKursnaLista(kursnaListaID);
                kursnaListaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = "Successfully deleted " + kursnaLista.ToString();
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
        /// Vraca opcije dostupne za rad sa kursnim listama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKursnaListaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
