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
    [Route("api/kupci")]
    public class KupacController : ControllerBase 
    {
        private readonly IFizickoLiceRepository fizickoLiceRepository;
        private readonly IPravnoLiceRepository pravnoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        /*  private readonly ILoggerService loggerService;
      private Message message = new Message();
      private readonly string serviceName = "KupacService";*/


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fizickoLiceRepository"></param>
        /// <param name="pravnoLiceRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public KupacController(IFizickoLiceRepository fizickoLiceRepository, IPravnoLiceRepository pravnoLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.fizickoLiceRepository = fizickoLiceRepository;
            this.pravnoLiceRepository = pravnoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }
        /// <summary>
        /// lista svih kupaca 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]

        public ActionResult<List<KupacModelDto>> GetKupci()
        {
            List<FizickoLice> fizickaLica = fizickoLiceRepository.GetFizickaLica();
            List<PravnoLice> pravnaLica = pravnoLiceRepository.getPravnaLica();
            /*message.ServiceName = serviceName;
        message.Method = "GET";*/
            List<KupacModel> sviKupci = fizickaLica.ConvertAll(f => (KupacModel)f);
            List<KupacModel> temp = pravnaLica.ConvertAll(f => (KupacModel)f);

            sviKupci.AddRange(temp); //objedinjena fizicka i pravna lica 

            /*       message.Information = "Returned list of ovlascena lica";
                 loggerService.CreateMessage(message);*/
            return Ok(mapper.Map<List<KupacModelDto>>(sviKupci));

        }


        /// <summary>
        /// Izbor jednog kupca na osnovu id-ja
        /// </summary>
        /// <param name="kupacId">unesite id kupca</param>
        /// <returns></returns>
        [HttpGet("{kupacId}")]
        public ActionResult<KupacModelDto> GetKupacById(Guid kupacId)
        {
            KupacModel kupac;


            /*   message.ServiceName = serviceName;
            message.Method = "GET";*/

            kupac = (KupacModel)fizickoLiceRepository.GetFizickoLiceById(kupacId); 

            if(kupac == null)  kupac = (KupacModel)pravnoLiceRepository.GetPravnoLiceById(kupacId);
            if (kupac == null)
            {
                /*   message.Information = "Not found";
              message.Error = "There is no object of Licnost with identifier: " + ovlascenoLiceId;
              loggerService.CreateMessage(message);*/
                return NotFound();
            }
            /*   message.Information = lice.ToString();
           loggerService.CreateMessage(message);*/
            return Ok(mapper.Map<KupacModelDto>(kupac));
                 
        }
        /// <summary>
        /// brisanje kupca na osnovu id-ja
        /// </summary>
        /// <param name="kupacId">unesite id kupca</param>
        /// <returns></returns>
        [HttpDelete("{kupacId}")]

        public IActionResult DeleteKupac(Guid kupacId)
        {
            /*message.ServiceName = serviceName;
          message.Method = "DELETE";*/

            try
            {
                KupacModel kupac;
                kupac = (KupacModel)fizickoLiceRepository.GetFizickoLiceById(kupacId);

                if (kupac == null) kupac = (KupacModel)pravnoLiceRepository.GetPravnoLiceById(kupacId);
                if (kupac == null)
                {
                    /*message.Information = "Not found";
                    message.Error = "There is no object of ovlasceno lice with identifier: " + ovlascenoLiceId;
                    loggerService.CreateMessage(message);*/
                    return NotFound();
                }

                if(kupac.FizPravno == true)
                {
                    fizickoLiceRepository.DeleteFizickoLice(kupacId);
                } else
                {
                    pravnoLiceRepository.DeletePravnoLice(kupacId);
                }
                // message.Information = "Successfully deleted "
                return NoContent();


            }
            catch (Exception)
            {
                /*       message.Information = "Server error";
               message.Error = ex.Message;
               loggerService.CreateMessage(message);*/
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        [HttpPut]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KupacModelDto> UpdateKupac(KupacModelDto kupac)
        {
            return NoContent();
            //do db
        }

        [HttpPost]
        public ActionResult<KupacModelDto> CreateKupac([FromBody] KupacModelDto kupac)
        {
            //    message.ServiceName = serviceName;
            //  message.Method = "POST";

            KupacModel k = mapper.Map<KupacModel>(kupac);
            KupacModel kupacCreated;

            if(k.FizPravno)
            {
                FizickoLice fizickoCreated = k as FizickoLice;
                kupacCreated = fizickoLiceRepository.CreateFizickoLice(fizickoCreated);
                fizickoLiceRepository.SaveChanges();
            } 
            else
            {
                PravnoLice pravnoCreated = k as PravnoLice;
                kupacCreated = pravnoLiceRepository.CreatePravnoLice(pravnoCreated);
                pravnoLiceRepository.SaveChanges();
            }

            string location = linkGenerator.GetPathByAction("GetKupci", "Kupac", new { KupacID = k.KupacID });
            /*
                message.Information = ovlascenoLice.ToString() + " | Ovlasceno lice location: " + location;
                loggerService.CreateMessage(message);*/
            return Created(location, mapper.Map<KupacModel>(kupacCreated));

        }

        /// <summary>
        /// opcije dostupne za kupca
        /// </summary>
        /// <returns></returns>

        [HttpOptions]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }



    }
}
