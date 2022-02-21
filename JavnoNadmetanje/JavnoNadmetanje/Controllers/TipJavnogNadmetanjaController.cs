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
    [Route("api/tipoviJavnihNadmetanja")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vracaju definisane formate
    public class TipJavnogNadmetanjaController : ControllerBase
    {
        private readonly ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "JavnoNadmetanjeService";
        private Message message = new Message();

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public TipJavnogNadmetanjaController(ITipJavnogNadmetanjaRepository tipJavnogNadmetanjaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.tipJavnogNadmetanjaRepository = tipJavnogNadmetanjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve tipove javnih nadmetanja.
        /// </summary>
        /// <returns> Lista tipova javnih nadmetanja</returns>
        /// <response code="200">Vraca listu tipova javnih nadmetanja</response>
        /// <response code="404">Nije pronadjen ni jedan tip javnog nadmetanja</response>
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<TipJavnogNadmetanjaDto>> GetTipoviJavnogNadmetanja()
        {
            List<TipJavnogNadmetanja> tipoviJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipoviJavnogNadmetanja();
            //ukoliko nije pronadjen ni jedan tip vratiti status 204(NoContent)
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (tipoviJavnogNadmetanja == null || tipoviJavnogNadmetanja.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of TipJavnogNadmetanja";
            loggerService.CreateMessage(message);
            //ukoliko smo pronasli neki tip vratiti status 200 i listu pronadjenih tipova
            return Ok(mapper.Map<List<TipJavnogNadmetanjaDto>>(tipoviJavnogNadmetanja));
        }

        /// <summary>
        /// Vraca jedan tip javnog nadmetanja na osnovu ID-ja.
        /// </summary>
        /// // <param name="tipJavnogNadmetanjaID">ID tipa javnog nadmetanja</param>
        /// <returns>Trazeni tip javnog nadmetanja</returns>
        /// <response code="200">Vraca trazeni tip javnog nadmetanje</response>
        /// <response code="404">Trazeni tip javnog nadmetanja nije pronadjeno</response>
        [HttpGet("{tipJavnogNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TipJavnogNadmetanjaDto> GetTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipJavnogNadmetanja tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaByID(tipJavnogNadmetanjaID);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (tipJavnogNadmetanja == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of TipJavnogNadmetanja with identifier: " + tipJavnogNadmetanjaID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = tipJavnogNadmetanja.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<TipJavnogNadmetanjaDto>(tipJavnogNadmetanja));
        }

        /// <summary>
        /// Kreira novi tip javnog nadmetanja.
        /// </summary>
        /// /// <param name="tipJavnogNadmetanjaDto">Model tipa javnog nadmetanja</param>
        /// <returns>Potvrdu o kreiranom tipu javnog nadmetanja.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa javnog nadmetanja \
        /// POST /api/tipoviJavnihNadmetanja \
        /// {   \
        ///    "nazivTipaJavnogNadmetanja": "Novi tip" \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiran tip javnog nadmetanja</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja tipa javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipJavnogNadmetanjaConfirmationDto> CreateTipJavnogNadmetanja([FromBody] TipJavnogNadmetanjaCreationDto tipJavnogNadmetanjaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                TipJavnogNadmetanja tip = mapper.Map<TipJavnogNadmetanja>(tipJavnogNadmetanjaDto);
                TipJavnogNadmetanjaConfirmationDto t = tipJavnogNadmetanjaRepository.CreateTipJavnogNadmetanja(tip);
                tipJavnogNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetTipJavnogNadmetanja", "TipJavnogNadmetanja", new { tipJavnogNadmetanjaID = t.TipJavnogNadmetanjaID });
                message.Information = tipJavnogNadmetanjaDto.ToString() + " | TipJavnogNadmetanja location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<TipJavnogNadmetanjaConfirmationDto>(t));
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
        /// Azurira jedan tip javnog nadmetanja.
        /// </summary>
        /// <param name="tipJavnogNadmetanjaDto">Model tipa javnog nadmetanja koji se azurira</param>
        /// <returns>Potvrdu o modifikovanom tipu javnog nadmetanja</returns>
        /// <response code="200">Vraca azuriran tip javnog nadmetanja</response>
        /// <response code="400">Tip javnog nadmetanja koji se azurira nije pronadjen</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja tipa javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipJavnogNadmetanjaConfirmationDto> UpdateTipJavnogNadmetanja(TipJavnogNadmetanjaUpdateDto tipJavnogNadmetanjaDto)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var starije = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaByID(tipJavnogNadmetanjaDto.TipJavnogNadmetanjaID);
                //provera da li postoji tip koji hocemo da azuriramo
                if (starije == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of TipJavnogNadmetanja with identifier: " + tipJavnogNadmetanjaDto.TipJavnogNadmetanjaID;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                TipJavnogNadmetanja tipJavnogNadmetanja = mapper.Map<TipJavnogNadmetanja>(tipJavnogNadmetanjaDto);
                //TipJavnogNadmetanjaConfirmationDto t = tipJavnogNadmetanjaRepository.UpdateTipJavnogNadmetanja(tipJavnogNadmetanja);
                mapper.Map(tipJavnogNadmetanja, starije);
                tipJavnogNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<TipJavnogNadmetanjaConfirmationDto>(starije));
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
        /// Vrsi brisanje jednog tipa javnog nadmetanja na osnovu ID-ja.
        /// </summary>
        /// <param name="tipJavnogNadmetanjaID">ID tipa javnog nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip javnog nadmetanja uspesno obrisan</response>
        /// <response code="404">Nije pronadjen tip javnog nadmetanja za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja tipa javnog nadmetanja</response>
        [HttpDelete("{tipJavnogNadmetanjaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                TipJavnogNadmetanja tipJavnogNadmetanja = tipJavnogNadmetanjaRepository.GetTipJavnogNadmetanjaByID(tipJavnogNadmetanjaID);
                if (tipJavnogNadmetanja == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of TipJavnogNadmetanja with identifier: " + tipJavnogNadmetanjaID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                tipJavnogNadmetanjaRepository.DeleteTipJavnogNadmetanja(tipJavnogNadmetanjaID);
                tipJavnogNadmetanjaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = "Successfully deleted " + tipJavnogNadmetanja.ToString();
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
        /// Vraca opcije dostupne za rad sa tipovima javnih nadmetanja.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetTipJavnogNadmetanjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
