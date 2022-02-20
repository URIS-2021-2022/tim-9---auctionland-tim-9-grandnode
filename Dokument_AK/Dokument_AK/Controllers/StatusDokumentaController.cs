using AutoMapper;
using Dokument_AK.Data;
using Dokument_AK.Entities;
using Dokument_AK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Controllers
{
    [ApiController]
    [Route("api/statusDok")]
    [Produces("application/json", "application/xml")]
    public class StatusDokumentaController : ControllerBase
    {
        private readonly IStatusDokumentaRepository statusDokRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="statusDokRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public StatusDokumentaController(IStatusDokumentaRepository statusDokRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.statusDokRepository = statusDokRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve statuse dokumenata
        /// </summary>
        /// <param name="Usvojen"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StatusDokumentaDto>> GetStatusDokumentaEnts(bool Usvojen=false)
        {
            List<StatusDokumentaEnt> statusdokEnts = statusDokRepository.GetStatusDokumentaEnts(Usvojen);
            if (statusdokEnts == null || statusdokEnts.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatusDokumentaDto>>(statusdokEnts));

        }


        /// <summary>
        /// Vraca status dokumenta na osnovu IDja
        /// </summary>
        /// <param name="statusDokID"></param>
        /// <returns></returns>

        [HttpGet("{statusDokID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StatusDokumentaDto> GetStatusDokumentaByID(Guid statusDokID)
        {
            StatusDokumentaEnt dokument = statusDokRepository.GetStatusDokumentaByID(statusDokID);

            if (dokument == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatusDokumentaDto>(dokument));
        }

        /// <summary>
        /// Kreira novi status dokumenta
        /// </summary>
        /// <param name="dokumentDto"></param>
        /// <returns></returns>

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusDokumentaDto> CreateStatusDokument([FromBody] StatusDokumentaCreationDto dokumentDto)
        {
            try
            {
                var dokument = mapper.Map<StatusDokumentaEnt>(dokumentDto);
                var confirmation = statusDokRepository.CreateStatusDokument(dokument);

                string location = linkGenerator.GetPathByAction("GetStatusDokumentaByID", "StatusDokumenta", new { statusDokID =  confirmation.StatusDokID});

                return Created(location, mapper.Map<StatusDokumentaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// Brise status dokumenta na osnovu IDja
        /// </summary>
        /// <param name="statusDokID"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{statusDokID}")]
        public IActionResult DeleteStatusDokumenta(Guid statusDokID)
        {
            try
            {
                StatusDokumentaEnt dok = statusDokRepository.GetStatusDokumentaByID(statusDokID);
                if (dok == null)
                {
                    return NotFound();
                }
                statusDokRepository.DeleteStatusDokumenta(statusDokID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azuriranje postojeceg status dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusDokumentaDto> UpdateStatusDokumenta(StatusDokumentaEnt dokument)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldDok = statusDokRepository.GetStatusDokumentaByID((Guid)dokument.StatusDokID);
                if (oldDok == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                StatusDokumentaEnt dok = mapper.Map<StatusDokumentaEnt>(dokument);

                mapper.Map(dok, oldDok); //Update objekta koji treba da sačuvamo u bazi                

                statusDokRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<StatusDokumentaDto>(oldDok));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        /// <summary>
        /// Opcije
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetStatusDokOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }

}

