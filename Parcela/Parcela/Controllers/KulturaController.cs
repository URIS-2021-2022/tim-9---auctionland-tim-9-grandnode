using Microsoft.AspNetCore.Mvc;
using Parcela.Data;
using Parcela.Models;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/kultura")]
    public class KulturaController : ControllerBase
    {
        private readonly IKulturaRepository kulturaRepository;
        private readonly IMapper mapper;

        public KulturaController(IKulturaRepository kulturaRepository, IMapper mapper)
        {
            this.kulturaRepository = kulturaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<KulturaDto>> GetKulturaList()
        {
            List<Kultura> kulturaLista = kulturaRepository.GetKulturaList();
            if (kulturaLista == null || kulturaLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KulturaDto>>(kulturaLista));
        }

        [HttpGet("{kulturaId}")]
        public ActionResult<KulturaDto> GetKulturaById(Guid klasaId)
        {
            Kultura kulturaModel = kulturaRepository.GetKulturaById(klasaId);
            if (kulturaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KulturaDto>(kulturaModel));
        }

        [HttpOptions]
        public IActionResult GetKulturaOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
