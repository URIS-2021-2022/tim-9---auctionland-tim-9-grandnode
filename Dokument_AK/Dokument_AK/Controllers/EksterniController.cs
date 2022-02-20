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
    [Route("api/eksterniDok")]
    [Produces("application/json", "application/xml")]
    public class EksterniController : ControllerBase
    {
        private readonly IEksterniDokumentRepository eksterniRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        public EksterniController(IEksterniDokumentRepository eksterniRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.eksterniRepository = eksterniRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        /// <summary>
        /// Vraca eksterne dokumente na osnovu filtera
        /// </summary>
        /// <param name="Izmenjen">Polje koje govori da li je dokument izmenjen</param>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<EksterniDokumentDto>> GetEksterniDokumentEnts(bool Izmenjen = false)
        {
            List<EksterniDokumentEnt> eks = eksterniRepository.GetEksterniDokumentEnts(Izmenjen);
            if (eks == null || eks.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<EksterniDokumentDto>>(eks));

        }

        /// <summary>
        /// Vraca dokument na osnv=ovu IDja
        /// </summary>
        /// <param name="dokumentID"></param>
        /// <returns></returns>

        [HttpGet("{dokumentID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EksterniDokumentDto> GetEskterniDokumentByID(Guid dokumentID)
        {
            EksterniDokumentEnt dokument = eksterniRepository.GetEskterniDokumentByID(dokumentID);

            if (dokument == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<EksterniDokumentDto>(dokument));
        }


        /// <summary>
        /// Kreiranje dokumenta
        /// </summary>
        /// <param name="dokumentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EksterniConfirmationDto> CreateEksterniDokument([FromBody] EksterniCreationDto dokumentDto)
        {
            try
            {
                var dokument = mapper.Map<EksterniDokumentEnt>(dokumentDto);
                var confirmation = eksterniRepository.CreateEksterniDokument(dokument);

                string location = linkGenerator.GetPathByAction("GetEskterniDokumentByID", "Eksterni", new { dokumentID = confirmation.DokumentID });

                return Created(location, mapper.Map<EksterniConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Brisanje dokumenta na osnovu IDja
        /// </summary>
        /// <param name="dokumentID"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{dokumentID}")]
        public IActionResult DeleteEksterniDokument(Guid dokumentID)
        {
            try
            {
                EksterniDokumentEnt dok = eksterniRepository.GetEskterniDokumentByID(dokumentID);
                if (dok == null)
                {
                    return NotFound();
                }
               eksterniRepository.DeleteEksterniDokument(dokumentID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        /// <summary>
        /// Azuriranje postojeceg dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EksterniDokumentDto> UpdateEksterniDokument(EksterniDokumentEnt dokument)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldDok = eksterniRepository.GetEskterniDokumentByID(dokument.DokumentID);
                if (oldDok == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                EksterniDokumentEnt dok = mapper.Map<EksterniDokumentEnt>(dokument);

                mapper.Map(dok, oldDok); //Update objekta koji treba da sačuvamo u bazi                

                eksterniRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<EksterniDokumentDto>(oldDok));
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
