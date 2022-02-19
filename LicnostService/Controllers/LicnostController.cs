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

        //Done
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<LicnostDto>> GetLicnosti() 
        {
            List<Licnost> licnosti = licnostRepository.GetLicnosti();

            if (licnosti.Count == 0) { return NoContent();  }

            return Ok(mapper.Map<List<LicnostDto>>(licnosti));
        }

        //Done
        [HttpGet("{licnostId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<LicnostDto> GetLicnostById(Guid licnostId) 
        {
            Licnost licnost = licnostRepository.GetLicnostById(licnostId);

            if (licnost == null) { return NotFound(); }

            return mapper.Map<LicnostDto>(licnost);
        }

        //Done
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicnostDto> CreateLicnost([FromBody] LicnostCUDto licnostDto) 
        {
            try
            {
                Licnost licnost = mapper.Map<Licnost>(licnostDto);
                licnost = licnostRepository.CreateLicnost(licnost);
                licnostRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetLicnostById", "Licnost", new { licnostId = licnost.LicnostId });

                return Created(location, mapper.Map<LicnostDto>(licnost));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u kreiranju: ");
            }
        }

        //Done
        [HttpDelete("{licnostId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLicnost(Guid licnostId) 
        {
            try
            {
            
                if (licnostRepository.GetLicnostById(licnostId) == null)
                {
                    return NotFound();
                }

                licnostRepository.DeleteLicnost(licnostId);
                licnostRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Uspesno brisanje!");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u brisanju!");
            }
        }

        //Done
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicnostDto> UpdateLicnost([FromBody] LicnostCUDto licnostDto)
        {
            try
            {
                Licnost staraLicnost = licnostRepository.GetLicnostById(licnostDto.LicnostId);
                if (staraLicnost == null)
                {
                    return NotFound();
                }

                Licnost licnost = mapper.Map<Licnost>(licnostDto);
                mapper.Map(licnost, staraLicnost);

                licnostRepository.SaveChanges();
                return Ok(mapper.Map<LicnostDto>(staraLicnost));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
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
