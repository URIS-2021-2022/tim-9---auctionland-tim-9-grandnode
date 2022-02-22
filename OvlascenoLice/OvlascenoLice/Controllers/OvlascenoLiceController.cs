using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OvlascenoLice.Data;
using OvlascenoLice.Entities;
using OvlascenoLice.Models;
using OvlascenoLice.ServiceCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Controllers
{
    [ApiController]
    [Route("api/ovlascenaLica")]
    public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository ovlascenoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IAdresaService adresaService;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly Message message = new Message();
        private readonly string serviceName = "OvlascenoLiceService";
        

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="ovlascenoLiceRepository"></param>
        /// <param name="loggerService"></param>
        /// <param name="adresaService"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public OvlascenoLiceController(IOvlascenoLiceRepository ovlascenoLiceRepository, ILoggerService loggerService, IAdresaService adresaService,LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ovlascenoLiceRepository = ovlascenoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.adresaService = adresaService;
            this.loggerService = loggerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Sva ovlascena lica 
        /// </summary>
        ///     /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="204">Nije pronađen ni jedna ličnost u sistemu</response>
        [HttpGet]
        [HttpHead]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<OvlascenoLiceDto>> GetOvlascenaLica()
        {
            List<OvlascenoLiceModel> lica = ovlascenoLiceRepository.GetOvlascenaLica();
            message.ServiceName = serviceName;
            message.Method = "GET";

            if (lica == null || lica.Count ==0) {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent(); 
            }

            try
            {
                foreach (OvlascenoLiceModel ol in lica)
                {
                    AdresaDto adresa = adresaService.GetAdresaById(ol.AdresaID).Result;

                    if (adresa != null)
                    {
                        ol.Adresa = adresa;
                    }
                }
            }
            catch (Exception ex)
            {
                return default;
            }

            message.Information = "Returned list of ovlascena lica";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<OvlascenoLiceDto>>(lica));
        }


        /// <summary>
        /// Vracanje samo jednog ovlascenog lica sa zadatim id-jem
        /// </summary>
        /// <param name="ovlascenoLiceId"></param>
        /// <returns></returns>
        [HttpGet("{ovlascenoLiceId}")]
        [Consumes("application/json")]
        public ActionResult<OvlascenoLiceDto> GetOvlascenoLiceById(Guid ovlascenoLiceId )
        {
            OvlascenoLiceModel lice = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
           
            message.ServiceName = serviceName;
            message.Method = "GET";

            if (lice == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Licnost with identifier: " + ovlascenoLiceId;
                loggerService.CreateMessage(message);
                return NotFound();

            }

            message.Information = lice.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<OvlascenoLiceDto>(lice));
        }



        /// <summary>
        /// Brisanje ovlascenog lica sa zadatim id-jem
        /// </summary>
        /// <param name="ovlascenoLiceId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("{ovlascenoLiceId}")]
        public IActionResult DeleteOvlascenoLice(Guid ovlascenoLiceId)
        {

            message.ServiceName = serviceName;
            message.Method = "DELETE";


            try
            {
                OvlascenoLiceModel lice = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
                if (lice == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of ovlasceno lice with identifier: " + ovlascenoLiceId;
                    loggerService.CreateMessage(message);
                    return NotFound();

                }

                ovlascenoLiceRepository.DeleteOvlascenoLice(ovlascenoLiceId);
                ovlascenoLiceRepository.SaveChanges();

                message.Information = "Successfully deleted " + ovlascenoLiceId.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + ovlascenoLiceId.ToString());

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
        /// Dodavanje novog ovlascenog lica
        /// </summary>
        /// <param name="ovlascenoLice">popunite ispravno model</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<OvlascenoLiceDto> CreateOvlascenoLice([FromBody] OvlascenoLiceDto ovlascenoLice)
        {

            message.ServiceName = serviceName;
            message.Method = "POST";


            try
            {
                Console.WriteLine("1");
                OvlascenoLiceModel lice1 = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
 
                Console.WriteLine("2");
                OvlascenoLiceModel createLice = ovlascenoLiceRepository.CreateOvlascenoLice(lice1);
                Console.WriteLine("3");
                ovlascenoLiceRepository.SaveChanges();
                Console.WriteLine("4");


                string location = linkGenerator.GetPathByAction("GetOvlascenoLiceById", "OvlascenoLice", new { ovlascenoLiceId = createLice.OvlascenoLiceID});

                message.Information = ovlascenoLice.ToString() + " | Ovlasceno lice location: " + location;
                loggerService.CreateMessage(message);


                return Created(location, mapper.Map<OvlascenoLiceDto>(createLice));
            }
            catch (Exception e)
            {
                message.Information = "Server error";
                message.Error = e.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "create Error");

            }
        }
       
        
        
        /// <summary>
        /// Azuriranje 
        /// </summary>
        /// <param name="ovlascenoLice">popunite ispravno model</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OvlascenoLiceDto> UpdateOvlascenoLice(OvlascenoLiceDto ovlascenoLice)
        {
             message.ServiceName = serviceName;
             message.Method = "PUT";

            try
            {
                OvlascenoLiceModel staroLice = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLice.OvlascenoLiceID);
                if (staroLice == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Licnost with identifier: " + ovlascenoLice.OvlascenoLiceID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                OvlascenoLiceModel novoLice = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
                mapper.Map(novoLice, staroLice);

                ovlascenoLiceRepository.SaveChanges();

                message.Information = staroLice.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<OvlascenoLiceDto>(staroLice));
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
        /// Opcije dostupne za Ovlasceno lice
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetPrioritetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }




    }
}
