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


        [HttpGet]
        //[HttpHead]

        public ActionResult<List<KontaktOsobaDto>> GetKontaktOsobe()
        {
            List<KontaktOsobaModel> kontaktOsobe = kontaktOsobaRepository.GetKontaktOsobe();
            if(kontaktOsobe == null || kontaktOsobe.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<KontaktOsobaDto>>(kontaktOsobe));
    
    }

        [HttpGet("{kontaktOsobaId}")]

        public ActionResult<KontaktOsobaDto> GetKontaktOsobaById(Guid kontaktOsobaId)
        {
            KontaktOsobaModel kontaktOsobaModel = kontaktOsobaRepository.GetKontaktOsobaById(kontaktOsobaId);
            if(kontaktOsobaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KontaktOsobaDto>(kontaktOsobaModel));
        }

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

        [HttpPost]
        public ActionResult<KontaktOsobaDto> UpdateKontaktOsoba(KontaktOsobaDto kontaktOsoba)
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
    }

}
