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
using ZalbaService.Models.TipZalbe;

namespace ZalbaService.Controllers
{
    [Route("api/tipzalbe")]
    [ApiController]
    [Authorize]
    public class TipZalbeController : ControllerBase
    {
        private readonly ITipZalbeRepository tipZalbeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public TipZalbeController(ITipZalbeRepository tipZalbeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipZalbeRepository = tipZalbeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<TipZalbeDto>> GetAllTipZalbe(string NazivTipa)
        {
            var tipZalbe = tipZalbeRepository.GetAllTipZalbe(NazivTipa);
            if (tipZalbe == null || tipZalbe.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TipZalbeDto>>(tipZalbe));
        }

        [HttpGet("{tipZalbeId}")]
        public ActionResult<TipZalbeDto> GetTipZalbe(Guid tipZalbeId)
        {
            var tipZalbe =  tipZalbeRepository.GetTipZalbeById(tipZalbeId);
            if(tipZalbe == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipZalbeDto>(tipZalbe));
        }

        [HttpPost]
        public ActionResult<TipZalbeDto> CreateTipZalbe([FromBody] TipZalbeCreationDto tipZalbe)
        {
            try
            {
                TipZalbe tipz = mapper.Map<TipZalbe>(tipZalbe);
                tipz = tipZalbeRepository.CreateTipZalbe(tipz);
                tipZalbeRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetTipZalbe", "TipZalbe", new { tipZalbeId = tipz.TipZalbeId });
                return Created(lokacija, mapper.Map<TipZalbeDto>(tipz));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja tipa zalbe!");
            }
        }

        [HttpPut]
        public ActionResult<TipZalbeDto> UpdateTipZalbe(TipZalbeUpdateDto tipZalbe)
        {
            try
            {
                var stariTipZ = tipZalbeRepository.GetTipZalbeById(tipZalbe.TipZalbeId);
                if(stariTipZ == null)
                {
                    return NotFound();
                }
                TipZalbe noviTipZ = mapper.Map<TipZalbe>(tipZalbe);
                mapper.Map(noviTipZ, stariTipZ);
                tipZalbeRepository.SaveChanges();

                return Ok(mapper.Map<TipZalbeDto>(stariTipZ));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene tipa zalbe!");
            }
        }

        [HttpDelete("{tipZalbeId}")]
        public IActionResult DeleteTipZalbe(Guid tipZalbeId)
        {
            try
            {
                var tipZalbe =  tipZalbeRepository.GetTipZalbeById(tipZalbeId);
                if(tipZalbe == null)
                {
                    return NotFound();
                }
                tipZalbeRepository.DeleteTipZalbe(tipZalbeId);
                tipZalbeRepository.SaveChanges();

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja tipa zalbe!");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetTipZalbeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }



    }
}
