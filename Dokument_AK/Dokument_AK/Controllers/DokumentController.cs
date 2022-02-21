using AutoMapper;
using Dokument_AK.Data;
using Dokument_AK.Entities;
using Dokument_AK.Models;
using Dokument_AK.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Controllers
{
    [ApiController]
    [Route("api/dokument")]
    
    [Produces("application/json", "application/xml")]
    public class DokumentController : ControllerBase
    {
        private readonly IDokumentRepository dokumentRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        private readonly ILoggerService loggerService;
        private readonly string serviceName = "Dokument_AK";
        private Message message = new Message();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="dokumentRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        /// <param name="linkGenerator"></param>
        public DokumentController(IDokumentRepository dokumentRepository, IMapper mapper, ILoggerService loggerService, LinkGenerator linkGenerator)
        {
            this.dokumentRepository = dokumentRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.linkGenerator = linkGenerator;
        }


        /// <summary>
        /// Vraca sve dokumente na osnovu prosledjenog filtera
        /// </summary>
        /// <param name="ZavodniBroj">Zavodni broj dokumenta</param>
        /// <returns>Lista dokumenata</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DokumentDto>> GetDokuments(string ZavodniBroj)
        {
            List<DokumentEnt> dokumentEnts = dokumentRepository.GetDokuments(ZavodniBroj);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (dokumentEnts == null || dokumentEnts.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Dokument";
            loggerService.CreateMessage(message);

            return Ok(mapper.Map<List<DokumentDto>>(dokumentEnts)); 
            
        }

        /// <summary>
        /// Vraca dokument na osnovu IDja
        /// </summary>
        /// <param name="dokumentID"></param>
        /// <returns></returns>

        [HttpGet("{dokumentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DokumentDto> GetDokumentByID(Guid dokumentID)
        {
            DokumentEnt dokument = dokumentRepository.GetDokumentByID(dokumentID);
            message.ServiceName = serviceName;
            message.Method = "GET";

            if (dokument == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Licnost with identifier: " + dokumentID;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = dokument.ToString();
            loggerService.CreateMessage(message);

            return Ok(mapper.Map<DokumentDto>(dokument));
        }

        /// <summary>
        /// Kreira dokument
        /// </summary>
        /// <param name="dokumentDto"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DokumentConfirmationDto> CreateDokument([FromBody] DokumentCreationDto dokumentDto)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";

            try
            {
                DokumentEnt dokument = mapper.Map<DokumentEnt>(dokumentDto);
                DokumentConfirmation confirmation = dokumentRepository.CreateDokument(dokument);

                string location = linkGenerator.GetPathByAction("GetDokumentByID", "Dokument", new { dokumentID = confirmation.DokumentID });

                message.Information = dokument.ToString() + " | Dokument location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<DokumentConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation error!");
            }
        }

        /// <summary>
        /// Brise dokument na osnovu IDja
        /// </summary>
        /// <param name="dokumentID">ID dokumenta</param>
        /// <returns></returns>

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{dokumentID}")]
        public IActionResult DeleteDokument(Guid dokumentID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                DokumentEnt dok = dokumentRepository.GetDokumentByID(dokumentID);
                if (dok == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Dokument with identifier: " + dokumentID;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }


                dokumentRepository.DeleteDokument(dokumentID);
                dokumentRepository.SaveChanges();

                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                message.Information = "Successfully deleted " + dok.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + dok.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Deletion error!");
            }
        }

/// <summary>
/// Azurira postojeci dokument
/// </summary>
/// <param name="dokument"></param>
/// <returns></returns>

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DokumentDto> UpdateDokument(DokumentEnt dokument)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldDok = dokumentRepository.GetDokumentByID(dokument.DokumentID);
                if (oldDok == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Dokument with identifier: " + dokument.DokumentID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                DokumentEnt dok = mapper.Map<DokumentEnt>(dokument);

                mapper.Map(dok, oldDok); //Update objekta koji treba da sačuvamo u bazi                

                dokumentRepository.SaveChanges(); //Perzistiramo promene
                message.Information = oldDok.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<DokumentDto>(oldDok));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
            }
        }

        /// <summary>
        /// Opcije
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetDokumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
