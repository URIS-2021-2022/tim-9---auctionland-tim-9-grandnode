using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Data;
using UgovorService.Entities;
using UgovorService.Models;
using UgovorService.ServiceCalls;

namespace UgovorService.Controllers
{
    [ApiController]
    [Route("api/ugovor")]
    [Produces("application/json", "application/xml")]
    public class UgovorController : ControllerBase
    {
        private readonly IUgovorRepository ugovorRepository;
        private readonly ILoggerService loggerService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly string serviceName = "UgovorService";
        private Message message = new Message();
        private readonly IDokument_AKService dokument_AKService;
        private readonly IKupac_SKService kupac_SKService;
        private readonly IJavnoNadmetanjeService javnoNadmetanjeService;


        public UgovorController(IUgovorRepository ugovorRepository, IMapper mapper, ILoggerService loggerService, LinkGenerator linkGenerator, IDokument_AKService dokument_AKService, IKupac_SKService kupac_SKService, IJavnoNadmetanjeService javnoNadmetanjeService)
        {
            this.ugovorRepository = ugovorRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.linkGenerator = linkGenerator;
            this.dokument_AKService = dokument_AKService;
            this.kupac_SKService = kupac_SKService;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<UgovorEnt>> GetUgovors(string ZavodniBr = null)
        {

            message.ServiceName = serviceName;
            message.Method = "GET";
            List<UgovorEnt> ugovori = ugovorRepository.GetUgovors(ZavodniBr);
            if (ugovori == null || ugovori.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            try
            {
                foreach (UgovorEnt u in ugovori)
                {
                    
                        DokumentDto dokument = dokument_AKService.GetDokumentByID(u.DokumentID).Result;
                        if (dokument != null)
                        {
                            u.DokumentDto = dokument;
                        }
                }
                foreach (UgovorEnt u in ugovori)
                {
                    KupacDto kupac = kupac_SKService.GetKupacById(u.KupacID).Result;
                    if(kupac != null)
                    {
                        u.KupacDto = kupac;
                    }
                }
                foreach (UgovorEnt u in ugovori)
                {
                    JavnoNadmetanjeDto nadmetanje = javnoNadmetanjeService.GetJavnoNadmetanjeByID(u.JavnoNadmetanjeID).Result;
                    if (nadmetanje != null)
                    {
                        u.JavnoNadmetanjeDto = nadmetanje;
                    }
                }
            }
            catch
            {
                return default;
            }

            message.Information = "Returned list of Ugovor";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<UgovorDto>>(ugovori));
        }

        [HttpGet("{ugovorID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UgovorEnt> GetUgovorByID(Guid ugovorID)
        {
            message.ServiceName = serviceName;
            message.Method = "GET";
            UgovorEnt ugo = ugovorRepository.GetUgovorByID(ugovorID);
            if (ugo == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Licnost with identifier: " + ugovorID;
                loggerService.CreateMessage(message);
                return NotFound();
                
            }
            message.Information = ugo.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<UgovorDto>(ugo));

        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UgovorConfirmationDto> CreateUgovor([FromBody] UgovorCreationDto ugovor)
        {

            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
              

                UgovorEnt ugo = mapper.Map<UgovorEnt>(ugovor);

                UgovorConfirmation confirmation = ugovorRepository.CreateUgovor(ugo);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetUgovorByID", "Ugovor", new { ugovorID = confirmation.UgovorID });
                message.Information = ugo.ToString() + " | Ugovor location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<UgovorConfirmationDto>(confirmation));
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation error!");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UgovorDto> UpdateUgovor(UgovorEnt ugovor)
        {

            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var oldg = ugovorRepository.GetUgovorByID(ugovor.UgovorID);

                if (oldg == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Ugovor with identifier: " + ugovor.UgovorID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                UgovorEnt ugo = mapper.Map<UgovorEnt>(ugovor);

                mapper.Map(ugo, oldg);

                ugovorRepository.SaveChanges();

                message.Information = oldg.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<UgovorDto>(oldg));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{ugovorID}")]
        public IActionResult DeleteUgovor(Guid ugovorID)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                UgovorEnt ugo = ugovorRepository.GetUgovorByID(ugovorID);
                if (ugo == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Ugovor with identifier: " + ugovorID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                UgovorEnt ugovor = ugovorRepository.GetUgovorByID(ugovorID);
                ugovorRepository.DeleteUgovor(ugovorID);
                ugovorRepository.SaveChanges();
                
                message.Information = "Successfully deleted " + ugovor.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + ugovor.ToString());
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Deletion error!");
            }
        }

        [HttpOptions]
        public IActionResult GetUgovorOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
