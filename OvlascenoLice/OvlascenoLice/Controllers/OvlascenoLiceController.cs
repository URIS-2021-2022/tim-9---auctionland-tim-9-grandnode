using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OvlascenoLice.Data;
using OvlascenoLice.Entities;
using OvlascenoLice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Controllers
{
    [ApiController]
    [Route("api/ovlascenaLica")]
    public class OvlascenoLiceController : ControllerBase
    {
        private readonly IOvlascenoLiceRepository ovlascenoLiceRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OvlascenoLiceController(IOvlascenoLiceRepository ovlascenoLiceRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ovlascenoLiceRepository = ovlascenoLiceRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Sva ovlascena lica 
        /// </summary>
        ///     /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="204">Nije pronađen ni jedna ličnost u sistemu</response>
        [HttpGet]
        [HttpHead]
        public ActionResult<List<OvlascenoLiceDto>> GetOvlascenaLica()
        {
            List<OvlascenoLiceModel> lica = ovlascenoLiceRepository.GetOvlascenaLica();
            if(lica == null || lica.Count ==0) { return NoContent(); }


            return Ok(mapper.Map<List<OvlascenoLiceDto>>(lica));
        }
        /// <summary>
        /// Vracanje samo jednog ovlascenog lica sa zadatim id-jem
        /// </summary>
        /// <param name="ovlascenoLiceId"></param>
        /// <returns></returns>
        [HttpGet("{ovlascenoLiceId}")]
        public ActionResult<OvlascenoLiceDto> GetOvlascenoLiceById(Guid ovlascenoLiceId )
        {
            OvlascenoLiceModel lice = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
            if(lice == null)
            {
                return NotFound();

            }
            return Ok(mapper.Map<OvlascenoLiceDto>(lice));
        }
        /// <summary>
        /// Brisanje ovlascenog lica sa zadatim id-jem
        /// </summary>
        /// <param name="ovlascenoLiceId"></param>
        /// <returns></returns>
        [HttpDelete("{ovlascenoLiceId}")]
        public IActionResult DeleteOvlascenoLice(Guid ovlascenoLiceId)
        {
            try
            {
                OvlascenoLiceModel lice = ovlascenoLiceRepository.GetOvlascenoLiceById(ovlascenoLiceId);
                if (lice == null)
                {
                    return NotFound();

                }

                ovlascenoLiceRepository.DeleteOvlascenoLice(ovlascenoLiceId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");

            }
        }
        /// <summary>
        /// Dodavanje novog ovlascenog lica
        /// </summary>
        /// <param name="ovlascenoLice">popunite ispravno model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<OvlascenoLiceDto> CreateOvlascenoLice([FromBody] OvlascenoLiceDto ovlascenoLice)
        {
            try
            {
                OvlascenoLiceModel lice1 = mapper.Map<OvlascenoLiceModel>(ovlascenoLice);
                OvlascenoLiceModel createLice = ovlascenoLiceRepository.CreateOvlascenoLice(lice1);
                string location = linkGenerator.GetPathByAction("GetOvlascenoLiceById", "OvlascenoLice", new { ovlascenoLiceId = lice1.OvlascenoLiceID});
                return Created(location, mapper.Map<OvlascenoLiceModel>(createLice));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "create Error");

            }
        }
        /// <summary>
        /// Azuriranje 
        /// </summary>
        /// <param name="ovlascenoLice">popunite ispravno model</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OvlascenoLiceDto> UpdateOvlascenoLice(OvlascenoLiceDto ovlascenoLice)
        {

            //baza 
            return NoContent();
        }

        /// <summary>
        /// Opcije dostupne za Ovlasceno lice
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetPrioritetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }




    }
}
