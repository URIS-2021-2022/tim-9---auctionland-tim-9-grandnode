using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Models.Radnja;

namespace ZalbaService.Controllers
{
    [Route("api/radnja")]
    [ApiController]
    [Authorize]
    public class RadnjaController : ControllerBase
    {

        private readonly IRadnjaRepository radnjaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public RadnjaController(IRadnjaRepository radnjaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.radnjaRepository = radnjaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<RadnjaDto>> GetAllRadnja(string NazivRadnje = null)
        {
            var radnja = radnjaRepository.GetAllRadnja(NazivRadnje);
            if (radnja == null || radnja.Count == 0)
            {
                return NoContent();
            }
           // var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            return Ok(mapper.Map<List<RadnjaDto>>(radnja));
        }

        [HttpGet("{radnjaId}")]
        public ActionResult<RadnjaDto> GetRadnja(Guid radnjaId)
        {
            var radnja =  radnjaRepository.GetRadnjaById(radnjaId);
            if (radnja == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RadnjaDto>(radnja));
        }

        [HttpPost]
        public  ActionResult<RadnjaDto> CreateRadnja([FromBody] RadnjaCreationDto radnja)
        {
            try
            {
                Radnja _radnja = mapper.Map<Radnja>(radnja);
                _radnja = radnjaRepository.CreateRadnja(_radnja);
                radnjaRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetRadnja", "Radnja", new { radnjaId = _radnja.RadnjaId });
                return Created(lokacija, mapper.Map<RadnjaDto>(_radnja));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja radnje!");
            }
        }

        [HttpPut]
        public ActionResult<RadnjaDto> UpdateRadnja(RadnjaUpdateDto radnja)
        {
            try
            {
                var staraRadnja = radnjaRepository.GetRadnjaById(radnja.RadnjaId);
                if (staraRadnja == null)
                {
                    return NotFound();
                }
                Radnja novaRadnja = mapper.Map<Radnja>(radnja);
                mapper.Map(novaRadnja, staraRadnja);
                radnjaRepository.SaveChanges();

                return Ok(mapper.Map<RadnjaDto>(staraRadnja));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene radnje!");
            }
        }

        [HttpDelete("{radnjaId}")]
        public IActionResult DeleteRadnja(Guid radnjaId)
        {
            try
            {
                var radnja = radnjaRepository.GetRadnjaById(radnjaId);
                if (radnja == null)
                {
                    return NotFound();
                }
                radnjaRepository.DeleteRadnja(radnjaId);
                radnjaRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja radnje!");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetRadnjaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
