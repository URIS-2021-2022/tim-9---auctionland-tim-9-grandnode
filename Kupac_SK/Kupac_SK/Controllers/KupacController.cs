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
{
    [ApiController]
    [Route("api/kupci")]
    public class KupacController : ControllerBase 
    {
        private readonly IFizickoLiceRepository fizickoLiceRepository;
        private readonly IPravnoLiceRepository pravnoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KupacController(IFizickoLiceRepository fizickoLiceRepository, IPravnoLiceRepository pravnoLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.fizickoLiceRepository = fizickoLiceRepository;
            this.pravnoLiceRepository = pravnoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

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

        [HttpGet("{kupacId}")]

        public ActionResult<KupacModelDto> GetKupacById(Guid kupacId)
        {
            KupacModel kupac;

            kupac = (KupacModel)fizickoLiceRepository.GetFizickoLiceById(kupacId); 

            if(kupac == null)  kupac = (KupacModel)pravnoLiceRepository.GetPravnoLiceById(kupacId);
            if(kupac == null) return NotFound();

            return Ok(mapper.Map<KupacModelDto>(kupac));
                 
        }

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
        public ActionResult<KupacModelDto> UpdateKupac(KupacModelDto kupac)
        {
            return NoContent();
            //do db
        }





        [HttpOptions]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }



    }
}
