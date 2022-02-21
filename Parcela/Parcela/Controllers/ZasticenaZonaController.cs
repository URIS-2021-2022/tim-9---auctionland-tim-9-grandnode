using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/zona")]
    [Produces("application/json")]
    public class ZasticenaZonaController : ControllerBase
    {
        private readonly IZasticenaZonaRepository zasticenaZonaRepository;
        private readonly IMapper mapper;

        public ZasticenaZonaController(IZasticenaZonaRepository zasticenaZonaRepository, IMapper mapper)
        {
            this.zasticenaZonaRepository = zasticenaZonaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ZasticenaZonaDto>> GetZasticenaZonaList()
        {
            List<ZasticenaZona> zasticenaZonaLista = zasticenaZonaRepository.GetZasticenaZonaList();
            if (zasticenaZonaLista == null || zasticenaZonaLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ZasticenaZonaDto>>(zasticenaZonaLista));
        }

        [HttpGet("{zonaId}")]
        public ActionResult<ZasticenaZonaDto> GetZasticenaZonaById(Guid zonaId)
        {
            ZasticenaZona zonaModel = zasticenaZonaRepository.GetZasticenaZonaById(zonaId);
            if (zonaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ZasticenaZonaDto>(zonaModel));
        }

        [HttpOptions]
        public IActionResult GetZonaOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
