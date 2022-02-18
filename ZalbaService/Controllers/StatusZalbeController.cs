using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Models.StatusZalbe;

namespace ZalbaService.Controllers
{
    [Route("api/statuszalbe")]
    [ApiController]
    [Authorize]
    public class StatusZalbeController : ControllerBase
    {
        private readonly IStatusZalbeRepository statusZalbeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public StatusZalbeController(IStatusZalbeRepository statusZalbeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.statusZalbeRepository = statusZalbeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<StatusZalbeDto>> GetAllStatusZalbe(string NazivStatusa)
        {
            var statusZalbe = statusZalbeRepository.GetAllStatusZalbe(NazivStatusa);
            if (statusZalbe == null || statusZalbe.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatusZalbeDto>>(statusZalbe));
        }

        [HttpGet("{statusZalbeId}")]
        public ActionResult<StatusZalbeDto> GetStatusZalbe(Guid statusZalbeId)
        {
            var statusZalbe = statusZalbeRepository.GetStatusZalbeById(statusZalbeId);
            if (statusZalbe == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatusZalbeDto>(statusZalbe));
        }

        [HttpPost]
        public ActionResult<StatusZalbeDto> CreateStatusZalbe([FromBody] StatusZalbeCreationDto statusZalbe)
        {
            try
            {
                StatusZalbe statusz = mapper.Map<StatusZalbe>(statusZalbe);
                statusz = statusZalbeRepository.CreateStatusZalbe(statusz);
                statusZalbeRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetStatusZalbe", "StatusZalbe", new { statusZalbeId = statusz.StatusZalbeId });
                return Created(lokacija, mapper.Map<StatusZalbeDto>(statusz));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja statusa zalbe!");
            }
        }

        [HttpPut]
        public ActionResult<StatusZalbeDto> UpdateStatusZalbe(StatusZalbeUpdateDto statusZalbe)
        {
            try
            {
                var stariStatusZ = statusZalbeRepository.GetStatusZalbeById(statusZalbe.StatusZalbeId);
                if (stariStatusZ == null)
                {
                    return NotFound();
                }
                StatusZalbe noviStatusZ = mapper.Map<StatusZalbe>(statusZalbe);
                mapper.Map(noviStatusZ, stariStatusZ);
                statusZalbeRepository.SaveChanges();

                return Ok(mapper.Map<StatusZalbeDto>(stariStatusZ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene statusa zalbe!");
            }
        }

        [HttpDelete("{statusZalbeId}")]
        public IActionResult DeleteStatusZalbe(Guid statusZalbeId)
        {
            try
            {
                var statusZalbe = statusZalbeRepository.GetStatusZalbeById(statusZalbeId);
                if (statusZalbe == null)
                {
                    return NotFound();
                }
                statusZalbeRepository.DeleteStatusZalbe(statusZalbeId);
                statusZalbeRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja statusa zalbe!");
            }
        }
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetStatusZalbeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
