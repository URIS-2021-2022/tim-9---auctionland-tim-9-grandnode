using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Data;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Controllers
{
    [ApiController]
    [Route("api/garancija")]
    [Produces("application/json", "application/xml")]
    public class TipGarancijeController : ControllerBase
    {
        private readonly ITipGarancijeRepository tipGarancijeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public TipGarancijeController(ITipGarancijeRepository tipGarancijeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipGarancijeRepository = tipGarancijeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TipGarancijeEnt>> GetGarancijes(string Tip = null)
        {
            List<TipGarancijeEnt> ugovori = tipGarancijeRepository.GetGarancijes(Tip);
            if (ugovori == null || ugovori.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TipGarancijeDto>>(ugovori));
        }

        [HttpGet("{tipID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TipGarancijeEnt> GetGarancijeByID(Guid tipID)
        {
            TipGarancijeEnt ugo = tipGarancijeRepository.GetGarancijeByID(tipID);
            if (ugo == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipGarancijeDto>(ugo));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipGarancijeConfirmationDto> CreateGarancije([FromBody] TipGarancijeCreationDto garancija)
        {
            try
            {
                TipGarancijeEnt ugo = mapper.Map<TipGarancijeEnt>(garancija);

                TipGarancijeConfirmation confirmation = tipGarancijeRepository.CreateGarancije(ugo);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetGarancijeByID", "TipGarancije", new { tipID = confirmation.TipID });
                return Created(location, mapper.Map<TipGarancijeConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipGarancijeDto> UpdateUgovor(TipGarancijeEnt garancija)
        {
            try
            {
                var oldg = tipGarancijeRepository.GetGarancijeByID(garancija.TipID);

                if (oldg ==  null)
                {
                    return NotFound();
                }
                TipGarancijeEnt ugo = mapper.Map<TipGarancijeEnt>(garancija);

                mapper.Map(ugo, oldg);            

                tipGarancijeRepository.SaveChanges();
                return Ok(mapper.Map<TipGarancijeDto>(oldg));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{tipID}")]
        public IActionResult DeleteUgovor(Guid tipID)
        {
            try
            {
                TipGarancijeEnt ugo = tipGarancijeRepository.GetGarancijeByID(tipID);
                if (ugo == null)
                {
                    return NotFound();
                }
                tipGarancijeRepository.DeleteGarancije(tipID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetGarancijeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
