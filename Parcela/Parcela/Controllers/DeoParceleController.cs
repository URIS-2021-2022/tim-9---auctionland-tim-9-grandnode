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

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/deoParcele")]
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
        public ActionResult<ParceleConfrimationDto> CreateDeoParcele([FromBody] DeoParceleCreateDto deoParcele)
        {
            try
            { 

                DeoParcele dp = mapper.Map<DeoParcele>(deoParcele);

                DeoParceleConfirmation confirmation = deoParceleRepository.CreateDeoParcele(dp);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetDeoParceleById", "DeoParcele", new { deoParceleID = confirmation.DeoParceleID });
                return Created(location, mapper.Map<ParceleConfrimationDto>(confirmation));
            }
            catch
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
        public ActionResult<ParceleConfrimationDto> UpdateDeoParcele(DeoParceleUpdateDto deoParcele)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (deoParceleRepository.GetDeoParcelaById(deoParcele.DeoParceleID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                DeoParcele dp = mapper.Map<DeoParcele>(deoParcele);
                DeoParceleConfirmation confirmation = deoParceleRepository.UpdateDeoParcele(dp);
                return Ok(mapper.Map<ParceleConfrimationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
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
