using AutoMapper;
using LicnostService.Data;
using LicnostService.Entities;
using LicnostService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Controllers
{
    [ApiController]
    [Route("api/licnosti")]
    public class LicnostController : ControllerBase
    {
        private readonly ILicnostRepository licnostRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public LicnostController(ILicnostRepository licnostRepository, IMapper mapper, LinkGenerator linkGenerator) 
        {
            this.licnostRepository = licnostRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<LicnostDto>> GetLicnosti() 
        {
            List<Licnost> licnosti = licnostRepository.GetLicnosti();

            if (licnosti.Count == 0) { return NoContent();  }

            return Ok(mapper.Map<List<LicnostDto>>(licnosti));
        }
        [HttpGet("{licnostId}")]
        public ActionResult<LicnostDto> GetLicnostById(Guid licnostId) 
        {
            Licnost licnost = licnostRepository.GetLicnostById(licnostId);

            if (licnost == null) { return NoContent(); }

            return mapper.Map<LicnostDto>(licnost);
        }

        [HttpPost]
        public ActionResult<LicnostDto> CreateLicnost([FromBody] LicnostCUDto licnostDto) 
        {
            try
            {
                Licnost licnost = mapper.Map<Licnost>(licnostDto);
                licnost = licnostRepository.CreateLicnost(licnost);
                string location = linkGenerator.GetPathByAction("GetLicnostById", "Licnost", new { licnostId = licnost.LicnostId });

                return Created("", mapper.Map<LicnostDto>(licnost));
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpDelete("{licnostId}")]
        public IActionResult DeleteLicnost(Guid licnostId) 
        {
            try
            {
            
                if (licnostRepository.GetLicnostById(licnostId) == null)
                {
                    return NotFound();
                }

                licnostRepository.DeleteLicnost(licnostId);
                return NoContent();
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<LicnostCUDto> UpdateLicnost(LicnostCUDto licnostUpdateDto)
        {
            try
            {
                if (licnostRepository.GetLicnostById(licnostUpdateDto.LicnostId) == null)
                {
                    return NotFound();
                }

                Licnost licnost = mapper.Map<Licnost>(licnostUpdateDto);
                licnost = licnostRepository.UpdateLicnost(licnost);
                return Ok(mapper.Map<LicnostDto>(licnost));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetLicnostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
