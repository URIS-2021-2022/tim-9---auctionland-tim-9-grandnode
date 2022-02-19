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
    [Route("api/opstina")]
    public class KatastarskaOpstinaController : ControllerBase
    {
        private readonly IKatastarskaOpstinaRepository katastarskaOpstinaRepository;
        private readonly IMapper mapper;

        public KatastarskaOpstinaController(IKatastarskaOpstinaRepository katastarskaOpstinaRepository, IMapper mapper)
        {
            this.katastarskaOpstinaRepository = katastarskaOpstinaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<KatastarskaOpstinaDto>> GetOpstinaList()
        {
            List<KatastarskaOpstina> opstinaLista = katastarskaOpstinaRepository.GetKatastarskaOpstinas();
            if (opstinaLista == null || opstinaLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KatastarskaOpstinaDto>>(opstinaLista));
        }

        [HttpGet("{opstinaId}")]
        public ActionResult<KatastarskaOpstinaDto> GetOpstinaById(Guid opstinaId)
        {
            KatastarskaOpstina opstinaModel = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(opstinaId);
            if (opstinaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KatastarskaOpstinaDto>(opstinaModel));
        }

        [HttpOptions]
        public IActionResult GetOpstinaOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
