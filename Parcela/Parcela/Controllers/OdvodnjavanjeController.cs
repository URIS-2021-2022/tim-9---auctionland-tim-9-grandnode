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
    [Route("api/odvodnjavanje")]
<<<<<<< Updated upstream
=======
    [Produces("application/json")]
>>>>>>> Stashed changes
    public class OdvodnjavanjeController : ControllerBase
    {
        private readonly IOdvodnjavanjeRepository odvodnjavanjeRepository;
        private readonly IMapper mapper;

        public OdvodnjavanjeController(IOdvodnjavanjeRepository odvodnjavanjeRepository, IMapper mapper)
        {
            this.odvodnjavanjeRepository = odvodnjavanjeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<OdvodnjavanjeDto>> GetOdvodnjavanjeList()
        {
            List<Odvodnjavanje> odvodnjavanjeLista = odvodnjavanjeRepository.GetOdvodnjavanjeList();
            if (odvodnjavanjeLista == null || odvodnjavanjeLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OdvodnjavanjeDto>>(odvodnjavanjeLista));
        }

        [HttpGet("{odvodnjavanjeId}")]
        public ActionResult<OdvodnjavanjeDto> GetOdvodnjavanjeById(Guid odvodnjavanjeId)
        {
            Odvodnjavanje odvodnjavanjeModel = odvodnjavanjeRepository.GetOdvodnjavanjeById(odvodnjavanjeId);
            if (odvodnjavanjeModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OdvodnjavanjeDto>(odvodnjavanjeModel));
        }

        [HttpOptions]
        public IActionResult GetOdvodnjavanjeOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
    }
}
