using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Models;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.ServiceCalls;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/deoParcele")]
    [Produces("application/json", "application/xml")]

    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository deoParceleRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;


        public DeoParceleController(IDeoParceleRepository deoParceleRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.deoParceleRepository = deoParceleRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<DeoParceleDto>> GetDeoParceleList()
        {
            List<DeoParcele> deoParceleLista = deoParceleRepository.GetDeoParceleList();
            if (deoParceleLista == null || deoParceleLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<DeoParceleDto>>(deoParceleLista));
        }

        [HttpGet("{deoParceleID}")]
        public ActionResult<DeoParceleDto> GetDeoParceleById(Guid deoParceleId)
        {
            DeoParcele deoParceleModel = deoParceleRepository.GetDeoParcelaById(deoParceleId);
            if (deoParceleModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DeoParceleDto>(deoParceleModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<DeoParceleDto> CreateDeoParcele([FromBody] DeoParceleDto deoParcele)

        { 
            try
            { 
                DeoParcele dp = mapper.Map<DeoParcele>(deoParcele);
                DeoParcele confirmation = deoParceleRepository.CreateDeoParcele(dp);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetDeoParceleList", "DeoParcele", new { DeoParceleID = confirmation.DeoParceleID });
                return Created(location, mapper.Map<DeoParceleDto>(confirmation));
            }
            catch (Exception)

            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{deoParceleID}")]
        public IActionResult DeleteDeoParcele(Guid deoParceleId)
        {
            try
            {
                DeoParcele deoParceleModel = deoParceleRepository.GetDeoParcelaById(deoParceleId);
                if (deoParceleModel == null)
                {
                    return NotFound();
                }
                deoParceleRepository.DeleteDeoParcele(deoParceleId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeoParceleDto> UpdateDeoParcele(DeoParceleDto deoParcele)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldDeoParcele = deoParceleRepository.GetDeoParcelaById(deoParcele.DeoParceleID);
                if (oldDeoParcele == null)
                {
                    return NotFound();
                }
                DeoParcele newDeoParcele = mapper.Map<DeoParcele>(deoParcele);

                mapper.Map(newDeoParcele, oldDeoParcele); //Update objekta koji treba da sačuvamo u bazi                

                deoParceleRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<DeoParceleDto>(newDeoParcele));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetDeoParceleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        
        
    }
}
