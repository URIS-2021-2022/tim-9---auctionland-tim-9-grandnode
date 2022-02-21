using AutoMapper;
using Mesto.Data;
using Mesto.Entities;
using Mesto.Models;
using Mesto.ServiceCalls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Controllers
{
    [ApiController]
    [Route("api/adresa")]
    [Produces("application/json", "application/xml")]

    public class AdresaController : ControllerBase
    {
        private readonly IAdresaRepository adresaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "Adresa";
        private Message message = new Message();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        /// <param name="linkGenerator"></param>
        public AdresaController(IAdresaRepository adresaRepository, LinkGenerator linkGenerator,IMapper mapper,ILoggerService loggerService)
        {
            this.adresaRepository = adresaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AdresaDto>> GetAdresaList()
        {
            var adrese = adresaRepository.GetAdresaList();
            message.ServiceName = serviceName;
            message.Method = "GET";

            if (adrese == null || adrese.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of Adresa";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<AdresaDto>>(adrese));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{adresaId}")]
        public ActionResult<AdresaDto> GetAdresaById(Guid adresaId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var adresa = adresaRepository.GetAdresaById(adresaId);
            message.ServiceName = serviceName;
            message.Method = "GET";

            if (adresa == null)

            {
                message.Information = "Not found";
                message.Error = "There is no object of Adresa with identifier: " + adresaId;
                loggerService.CreateMessage(message); 
                return NotFound();
            }

            message.Information = adresa.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<AdresaDto>(adresa));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<AdresaDto> CreateAdresa([FromBody] AdresaDto adresa)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";

            try
            {
                Adresa a = mapper.Map<Adresa>(adresa);
                Adresa confirmation = adresaRepository.CreateAdresa(a);

                string location = linkGenerator.GetPathByAction("GetAdresaById", "Adresa", new { adresaId = confirmation.AdresaId });

                message.Information = a.ToString() + " | Adresa location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<AdresaDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation error!");
            }
        }

        [HttpDelete("{adresaId}")]
        public IActionResult DeleteAdresa(Guid adresaId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";  
            try
            {
                Adresa adresa = adresaRepository.GetAdresaById(adresaId);
                if (adresa == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Adresa with identifier: " + adresaId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }
                
                
                adresaRepository.DeleteAdresa(adresaId);
                adresaRepository.SaveChanges();
                message.Information = "Successfully deleted " + adresa.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + adresa.ToString());
            }
            catch(Exception ex)
            {

                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja adrese!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<AdresaDto> UpdateAdresa(Adresa adresa)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldAdresa = adresaRepository.GetAdresaById(adresa.AdresaId);
                if (oldAdresa == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Adresa with identifier: " + adresa.AdresaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
               Adresa adresaNew = mapper.Map<Adresa>(adresa);

                mapper.Map(adresaNew, oldAdresa); //Update objekta koji treba da sačuvamo u bazi                

                adresaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = adresaNew.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<Adresa>(adresaNew));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
            }
        }
        [HttpOptions]
        public IActionResult GetAdresaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }

    }
    }

