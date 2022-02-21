using AutoMapper;
using Kupac_SK.Data;
using Kupac_SK.Entities;
using Kupac_SK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Controllers
{/// <summary>
/// 
/// </summary>
    [ApiController]
    [Route("api/prioriteti")]
    public class PrioritetController : ControllerBase
    {
        private readonly IPrioritetRepository prioritetRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        /*  private readonly ILoggerService loggerService;
        private Message message = new Message();
        private readonly string serviceName = "KupacService";*/


        /// <summary>
        /// 
        /// </summary>
        /// <param name="prioritetRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public PrioritetController(IPrioritetRepository prioritetRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.prioritetRepository = prioritetRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraca listu prioriteta koji postoje evidentirani
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        public ActionResult<List<PrioritetModelDto>> GetPrioritetiList()
        {
            List<PrioritetModel> prioriteti = prioritetRepository.GetPrioriteti();
            /*message.ServiceName = serviceName;
            message.Method = "GET";*/
            if (prioriteti == null || prioriteti.Count == 0)
            {
                /*
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message); */
                return NoContent();
            }
            /*       message.Information = "Returned list of ovlascena lica";
            loggerService.CreateMessage(message);*/
            return Ok(mapper.Map<List<PrioritetModelDto>>(prioriteti));
        }



        /// <summary>
        /// vraca prioritet na osnovu prosledjenog id-ja
        /// </summary>
        /// <param name="prioritetId">unesite validan id</param>
        /// <returns></returns>
        [HttpGet("{prioritetId}")]
        public ActionResult<PrioritetModelDto> GetPrioritetById(Guid prioritetId)
        {
            PrioritetModel prioritetModel = prioritetRepository.GetPrioritetById(prioritetId);
           
            /*   message.ServiceName = serviceName;
            message.Method = "GET";*/

            if (prioritetModel == null)
            {
                /*   message.Information = "Not found";
                message.Error = "There is no object of Licnost with identifier: " + ovlascenoLiceId;
                loggerService.CreateMessage(message);*/
                return NotFound();
            }
            /*   message.Information = lice.ToString();
            loggerService.CreateMessage(message);*/
            return Ok(mapper.Map<PrioritetModelDto>(prioritetModel));
        }
       /// <summary>
       /// brisanje prioriteta 
       /// </summary>
       /// <param name="prioritetId">unesite validan id</param>
       /// <returns></returns>
        [HttpDelete("{prioritetId}")]
        public IActionResult DeletePrioritet(Guid prioritetId)
        {
            /*message.ServiceName = serviceName;
            message.Method = "DELETE";*/
            try
            {
                PrioritetModel prioritetModel = prioritetRepository.GetPrioritetById(prioritetId);
                if (prioritetModel == null)
                {
                    /*message.Information = "Not found";
                    message.Error = "There is no object of ovlasceno lice with identifier: " + ovlascenoLiceId;
                    loggerService.CreateMessage(message);*/
                    return NotFound();
                }
                prioritetRepository.DeletePrioritet(prioritetId);
                return NoContent();

                // message.Information = "Successfully deleted " + ovlascenoLiceId.ToString();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
            }
            catch (Exception ex)
            {
                /*       message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);*/
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }

        }


        /// <summary>
        /// unos novog prioriteta
        /// </summary>
        /// <param name="prioritet">popunite ispravno</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PrioritetModelDto> CreatePrioritet([FromBody] PrioritetModelDto prioritet) 
        {/*
            message.ServiceName = serviceName;
            message.Method = "POST";*/
            try
            {
               
                PrioritetModel prior = mapper.Map<PrioritetModel>(prioritet);
                PrioritetModel prioritetCreate = prioritetRepository.CreatePrioritet(prior);
                prioritetRepository.SaveChanges();
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPrioritetById", "Prioritet", new { prioritetId = prior.PrioritetID });
               // Console.Write(location);

                /*
                message.Information = ovlascenoLice.ToString() + " | Ovlasceno lice location: " + location;
                loggerService.CreateMessage(message);*/

                return Created(location, mapper.Map<PrioritetModel>(prioritetCreate));
                //map u dto 

            }
            catch
            {/*
                message.Information = "Server error";
                message.Error = e.Message;
                loggerService.CreateMessage(message);*/
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="prioritet"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PrioritetModelDto> UpdatePrioritet(PrioritetModelDto prioritet)
        {/*
            message.ServiceName = serviceName;
            message.Method = "PUT";*/

            try
            {
                PrioritetModel stariPr = prioritetRepository.GetPrioritetById(prioritet.PrioritetID);
                if(stariPr ==null)
                {
                    /* message.Information = "Not found";
                    message.Error = "There is no object of Licnost with identifier: " + ovlascenoLice.OvlascenoLiceID;
                    loggerService.CreateMessage(message);*/
                    return NotFound();
                }

                PrioritetModel noviPr = mapper.Map<PrioritetModel>(prioritet);
                mapper.Map(noviPr, stariPr);

                prioritetRepository.SaveChanges();
                /*
                message.Information = staroLice.ToString();
                loggerService.CreateMessage(message);*/

                return Ok(mapper.Map<PrioritetModelDto>(stariPr));

            }
            catch (Exception e)
            { /*
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);*/
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
            }
        }
        /// <summary>
        /// ponudjene opcije za kupca
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
