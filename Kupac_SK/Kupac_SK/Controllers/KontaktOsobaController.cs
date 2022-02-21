using AutoMapper;
using Kupac_SK.Data;
using Kupac_SK.Entities;
using Kupac_SK.Models;
using Kupac_SK.ServiceCalls_;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Controllers
{
    [ApiController]
    [Route("api/kontaktOsobe")]

    public class KontaktOsobaController : ControllerBase 
    {
        
        private readonly IKontaktOsobaRepository kontaktOsobaRepository;
        private readonly  LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private Message message = new Message();
        private readonly string serviceName = "KupacService";

        public KontaktOsobaController(IKontaktOsobaRepository kontaktOsobaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kontaktOsobaRepository = kontaktOsobaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }

        /// <summary>
        /// izlistavanje kontakt osoba 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<KontaktOsobaDto>> GetKontaktOsobe()
        {
            List<KontaktOsobaModel> kontaktOsobe = kontaktOsobaRepository.GetKontaktOsobe();

            message.ServiceName = serviceName;
            message.Method = "GET";

            if (kontaktOsobe == null || kontaktOsobe.Count == 0)
            {

                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.Information = "Returned list of prioriteti";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<KontaktOsobaDto>>(kontaktOsobe));
    
    }
        
        
        /// <summary>
        /// pronadjite osobu na osnovu id-ja
        /// </summary>
        /// <param name="kontaktOsobaId"></param>
        /// <returns></returns>
        [HttpGet("{kontaktOsobaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<KontaktOsobaDto> GetKontaktOsobaById(Guid kontaktOsobaId)
        {
            KontaktOsobaModel kontaktOsobaModel = kontaktOsobaRepository.GetKontaktOsobaById(kontaktOsobaId);
          
            message.ServiceName = serviceName;
            message.Method = "GET";

            if (kontaktOsobaModel == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of kontakt osoba with identifier: " + kontaktOsobaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }

            message.Information = prioritetModel.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<KontaktOsobaDto>(kontaktOsobaModel));
        }
        /// <summary>
        /// izbrisite osobu
        /// </summary>
        /// <param name="kontaktOsobaId"></param>
        /// <returns></returns>
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{kontaktOsobaId}")]

        public IActionResult DeleteKontaktOsoba(Guid kontaktOsobaId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";

            try
            {
                KontaktOsobaModel kontaktOsoba = kontaktOsobaRepository.GetKontaktOsobaById(kontaktOsobaId);

                if(kontaktOsoba ==null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of kontakt osoba with identifier: " + kontaktOsobaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                kontaktOsobaRepository.DeleteKontaktOsoba(kontaktOsobaId);
                message.Information = "Successfully deleted " + prioritetId.ToString();
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
        /// 
        /// </summary>
        /// <param name="kontaktOsoba"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KontaktOsobaDto> CreateKontaktOsoba(KontaktOsobaDto kontaktOsoba)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
            
                KontaktOsobaModel kont1 = mapper.Map<KontaktOsobaModel>(kontaktOsoba);
                KontaktOsobaModel kontaktCreate = kontaktOsobaRepository.CreateKontaktOsoba(kont1);
                kontaktOsobaRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetKontaktOsobaById", "KontaktOsoba", new { kontaktOsobaId = kontaktCreate.KontaktOsobaID });

                message.Information = kontaktOsoba.ToString() + " | kontakt osoba location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<KontaktOsobaModel>(kontaktOsoba));
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
        /// Opcije omogucene za Kontakt Osobu
        /// </summary>
        /// <returns></returns>
        /// 

     
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KontaktOsobaDto> UpdateKontaktOsoba(KontaktOsobaDto kontaktOsoba)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";

            KontaktOsobaModel stara = kontaktOsobaRepository.GetKontaktOsobaById(kontaktOsoba.KontaktOsobaID);
            if(stara == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of kontakt osoba with identifier: " + kontaktOsoba.KontaktOsobaID;
                loggerService.CreateMessage(message);
                return NotFound();
            }

            KontaktOsobaModel nova = mapper.Map<KontaktOsobaModel>(kontaktOsoba);
            mapper.Map(nova, stara);

            kontaktOsobaRepository.SaveChanges();

            message.Information = stara.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<KontaktOsobaDto>(stara));

        }
        [HttpOptions]
        public IActionResult GetKontaktOsobaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }

}
