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
    [Route("api/interniDok")]
    [Produces("application/json", "application/xml")]

    public class InterniController : ControllerBase
    {
        private readonly IInterniDokumentRepository interniRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="interniRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public InterniController(IInterniDokumentRepository interniRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.interniRepository = interniRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraca sve dokumente
        /// </summary>
        /// <param name="Izmenjen"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<InterniDokumentDto>> GetInterniDokumentEnts(bool Izmenjen = false)
        {
            List<InterniDokumentEnt> interniEnts = interniRepository.GetInterniDokumentEnts(Izmenjen);
            if (interniEnts == null || interniEnts.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<InterniDokumentDto>>(interniEnts));

        }

        /// <summary>
        /// Vraca dokumente na osnovu IDja
        /// </summary>
        /// <param name="dokumentID"></param>
        /// <returns></returns>

        [HttpGet("{dokumentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InterniDokumentDto> GetInterniDokumentByID(Guid dokumentID)
        {
            InterniDokumentEnt dokument = interniRepository.GetInterniDokumentByID(dokumentID);

            if (dokument == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<InterniDokumentDto>(dokument));
        }


        /// <summary>
        /// Kreiranje dokumenta
        /// </summary>
        /// <param name="dokumentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InterniConfirmationDto> CreateInterniDokument([FromBody] InterniCreationDto dokumentDto)
        {
            try
            {
                var dokument = mapper.Map<InterniDokumentEnt>(dokumentDto);
                var confirmation = interniRepository.CreateInterniDokument(dokument);

                string location = linkGenerator.GetPathByAction("GetInterniDokumentByID", "Interni", new { dokumentID = confirmation.DokumentID });

                return Created(location, mapper.Map<InterniConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Brisanje dokumenta
        /// </summary>
        /// <param name="dokumentID"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{dokumentID}")]
        public IActionResult DeleteInterniDokument(Guid dokumentID)
        {
            try
            {
               InterniDokumentEnt dok = interniRepository.GetInterniDokumentByID(dokumentID);
                if (dok == null)
                {
                    return NotFound();
                }
               interniRepository.DeleteInterniDokument(dokumentID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azuriranje dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InterniDokumentDto> UpdateInterniDokument(InterniDokumentEnt dokument)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldDok = interniRepository.GetInterniDokumentByID(dokument.DokumentID);
                if (oldDok == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                InterniDokumentEnt dok = mapper.Map<InterniDokumentEnt>(dokument);

                mapper.Map(dok, oldDok); //Update objekta koji treba da sačuvamo u bazi                

                interniRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<InterniDokumentDto>(oldDok));
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
        public IActionResult GetInterniOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

