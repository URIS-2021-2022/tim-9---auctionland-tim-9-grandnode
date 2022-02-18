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
using ZalbaService.Models.Zalba;

namespace ZalbaService.Controllers
{
    [Route("api/zalba")]
    [ApiController]
    [Authorize]
    public class ZalbaController : ControllerBase
    {
        private readonly IZalbaRepository zalbaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ZalbaController(IZalbaRepository zalbaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zalbaRepository = zalbaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ZalbaDto>> GetAllZalba()
        {
            var zalba = zalbaRepository.GetAllZalba();
            if (zalba == null || zalba.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ZalbaDto>>(zalba));
        }

        [HttpGet("{zalbaId}")]
        public ActionResult<ZalbaDto> GetZalba(Guid zalbaId)
        {
            var zalba = zalbaRepository.GetZalbaById(zalbaId);
            if (zalba == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ZalbaDto>(zalba));
        }

        [HttpPost]
        public ActionResult<ZalbaDto> CreateZalba([FromBody] ZalbaCreationDto zalba)
        {
            try
            {
                Zalba _zalba = mapper.Map<Zalba>(zalba);
                _zalba = zalbaRepository.CreateZalba(_zalba);
                zalbaRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetZalba", "Zalba", new { zalbaId = _zalba.ZalbaId });
                return Created(lokacija, mapper.Map<ZalbaDto>(_zalba));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja zalbe!");
            }
        }

        [HttpPut]
        public ActionResult<ZalbaDto> UpdateZalba(ZalbaUpdateDto zalba)
        {
            try
            {
                var staraZalba = zalbaRepository.GetZalbaById(zalba.ZalbaId);
                if (staraZalba == null)
                {
                    return NotFound();
                }
                Zalba novaZalba = mapper.Map<Zalba>(zalba);
                mapper.Map(novaZalba, staraZalba);
                zalbaRepository.SaveChanges();

                return Ok(mapper.Map<ZalbaDto>(staraZalba));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene zalbe!");
            }
        }

        [HttpDelete("{zalbaId}")]
        public IActionResult DeleteZalba(Guid zalbaId)
        {
            try
            {
                var zalba = zalbaRepository.GetZalbaById(zalbaId);
                if (zalba == null)
                {
                    return NotFound();
                }
                zalbaRepository.DeleteZalba(zalbaId);
                zalbaRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja zalbe!");
            }
        }
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetZalbaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
