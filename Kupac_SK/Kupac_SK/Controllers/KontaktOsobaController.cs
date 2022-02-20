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
    [Route("api/kontaktOsobe")]

   

    public class KontaktOsobaController : ControllerBase 
    {
        
        private readonly IKontaktOsobaRepository kontaktOsobaRepository;
        private readonly  LinkGenerator linkGenerator;
        private readonly IMapper mapper;

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
            if(kontaktOsobe == null || kontaktOsobe.Count == 0)
            {
                return NoContent();
            }

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
            if(kontaktOsobaModel == null)
            {
                return NotFound();
            }
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
            try
            {
                KontaktOsobaModel kontaktOsoba = kontaktOsobaRepository.GetKontaktOsobaById(kontaktOsobaId);

                if(kontaktOsoba ==null)
                {
                    return NotFound();
                }

                kontaktOsobaRepository.DeleteKontaktOsoba(kontaktOsobaId);
                return NoContent();
            }
            catch
            {
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
            try
            {
            
                KontaktOsobaModel kont1 = mapper.Map<KontaktOsobaModel>(kontaktOsoba);
                KontaktOsobaModel kontaktCreate = kontaktOsobaRepository.CreateKontaktOsoba(kont1);

                string location = linkGenerator.GetPathByAction("GetKontaktOsobaById", "KontaktOsoba", new { kontaktOsobaId = kontaktCreate.KontaktOsobaID });
                return Created(location, mapper.Map<KontaktOsobaModel>(kontaktOsoba));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");

            }
        }
        /// <summary>
        /// Opcije omogucene za Kontakt Osobu
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetKontaktOsobaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }

}
