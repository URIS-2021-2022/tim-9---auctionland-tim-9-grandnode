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

            List<KupacModel> sviKupci = fizickaLica.ConvertAll(f => (KupacModel)f);
            List<KupacModel> temp = pravnaLica.ConvertAll(f => (KupacModel)f);

            sviKupci.AddRange(temp); //objedinjena fizicka i pravna lica 

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

            kupac = (KupacModel)fizickoLiceRepository.GetFizickoLiceById(kupacId); 

            if(kupac == null)  kupac = (KupacModel)pravnoLiceRepository.GetPravnoLiceById(kupacId);
            if(kupac == null) return NotFound();

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
            try
            {
                KupacModel kupac;
                kupac = (KupacModel)fizickoLiceRepository.GetFizickoLiceById(kupacId);

                if (kupac == null) kupac = (KupacModel)pravnoLiceRepository.GetPravnoLiceById(kupacId);
                if (kupac == null) return NotFound();

                if(kupac.FizPravno == true)
                {
                    fizickoLiceRepository.DeleteFizickoLice(kupacId);
                } else
                {
                    pravnoLiceRepository.DeletePravnoLice(kupacId);
                }
                return NoContent();


            }
            catch (Exception)
            {

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
