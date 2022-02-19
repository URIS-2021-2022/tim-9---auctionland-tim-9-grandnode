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
    [Route("api/klasa")]
    public class KlasaController : ControllerBase
    {
        private readonly IKlasaRepository klasaRepository;
        private readonly IMapper mapper;

        public KlasaController(IKlasaRepository klasaRepository, IMapper mapper)
        {
            this.klasaRepository = klasaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<KlasaDto>> GetKlasaList()
        {
            List<Klasa> klasaLista = klasaRepository.GetKlasaList();
            if (klasaLista == null || klasaLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KlasaDto>>(klasaLista));
        }

        [HttpGet("{klasaId}")]
        public ActionResult<KlasaDto> GetKlasaById(Guid klasaId)
        {
            Klasa klasaModel = klasaRepository.GetKlasaById(klasaId);
            if (klasaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KlasaDto>(klasaModel));
        }

        [HttpOptions]
        public IActionResult GetKlasaOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
