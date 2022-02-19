using Microsoft.AspNetCore.Mvc;
using Parcela.Models;
using Parcela.Data;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/oblik-svojine")]
    public class OblikSvojineController : ControllerBase
    {
        private readonly IOblikSvojineRepository oblikSvojineRepository;
        private readonly IMapper mapper;

        public OblikSvojineController(IOblikSvojineRepository oblikSvojineRepository, IMapper mapper)
        {
            this.oblikSvojineRepository = oblikSvojineRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<OblikSvojineDto>> GetSvojinaList()
        {
            List<OblikSvojine> svojinaLista = oblikSvojineRepository.GetOblikSvojineList();
            if (svojinaLista == null || svojinaLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OblikSvojineDto>>(svojinaLista));
        }

        [HttpGet("{svoijaId}")]
        public ActionResult<OblikSvojineDto> GetSvojinaById(Guid svojinaId)
        {
            OblikSvojine svojinaModel = oblikSvojineRepository.GetOblikSvojineById(svojinaId);
            if (svojinaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OblikSvojineDto>(svojinaModel));
        }

        [HttpOptions]
        public IActionResult GetSvojinaOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
