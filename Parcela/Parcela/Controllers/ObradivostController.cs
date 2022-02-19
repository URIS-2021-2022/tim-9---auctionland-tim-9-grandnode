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
    [Route("api/obradivost")]
<<<<<<< Updated upstream
=======
    [Produces("application/json")]
>>>>>>> Stashed changes
    public class ObradivostController : ControllerBase
    {
        private readonly IObradivostRepository obradivostRepository;
        private readonly IMapper mapper;

        public ObradivostController(IObradivostRepository obradivostRepository, IMapper mapper)
        {
            this.obradivostRepository = obradivostRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ObradivostDto>> GetObradivostList()
        {
            List<Obradivost> obradivostLista = obradivostRepository.GetObradivostList();
            if (obradivostLista == null || obradivostLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ObradivostDto>>(obradivostLista));
        }

        [HttpGet("{obradivostId}")]
        public ActionResult<ObradivostDto> GetObradivostById(Guid obradivostId)
        {
            Obradivost obradivostModel = obradivostRepository.GetObradivostById(obradivostId);
            if (obradivostModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ObradivostDto>(obradivostModel));
        }

        [HttpOptions]
        public IActionResult GetObradivostOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
