using AutoMapper;
using LicnostService.Data;
using LicnostService.Entities;
using LicnostService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Controllers
{
    [ApiController]
    [Route("api/licnost")]
    public class LicnostController : ControllerBase
    {
        private readonly ILicnostRepository licnostRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public LicnostController(ILicnostRepository licnostRepository, IMapper mapper, LinkGenerator linkGenerator) 
        {
            this.licnostRepository = licnostRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Vraća sve ličnosti.
        /// </summary>
        /// <returns>Lista ličnosti</returns>
        /// <response code="200">Vraća listu ličnosti</response>
        /// <response code="204">Nije pronađen ni jedna ličnost u sistemu</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<LicnostDto>> GetLicnosti() 
        {
            List<Licnost> licnosti = licnostRepository.GetLicnosti();

            if (licnosti.Count == 0) { return NoContent();  }

            return Ok(mapper.Map<List<LicnostDto>>(licnosti));
        }

        /// <summary>
        /// Vraća ličnost na osnovu identifikatora ličnost.
        /// </summary>
        /// <param name="licnostId">Identifikator licnosti (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>Ličnost</returns>
        /// <response code="200">Vraća ličnost koja je pronađena</response>
        /// <response code="204">Ne postoji ličnost sa datim identifikatorom</response>
        [HttpGet("{licnostId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<LicnostDto> GetLicnostById(Guid licnostId) 
        {
            Licnost licnost = licnostRepository.GetLicnostById(licnostId);

            if (licnost == null) { return NotFound(); }

            return mapper.Map<LicnostDto>(licnost);
        }

        /// <summary>
    /// Upisuje ličnost.
    /// </summary>
    /// <param name="licnostDto">Model ličnosti</param>
    /// <returns>Podatke o ličnosti koja je upisana</returns>
    /// <remarks>
    /// Primer zahteva za upis ličnosti \
    /// POST /api/licnost \
    /// {
    ///     "Ime": "Milutina",
    ///     "Prezime": "Milankovic",
    ///     "Funkcija": "Clan"
    /// }
    /// </remarks>
    /// <response code="201">Vraća podatke o upisanoj ličnosti</response>
    /// <response code="500">Postoji neki problem sa upisom</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicnostDto> CreateLicnost([FromBody] LicnostCUDto licnostDto) 
        {
            try
            {
                Licnost licnost = mapper.Map<Licnost>(licnostDto);
                licnost = licnostRepository.CreateLicnost(licnost);
                licnostRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetLicnostById", "Licnost", new { licnostId = licnost.LicnostId });

                return Created(location, mapper.Map<LicnostDto>(licnost));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u kreiranju: ");
            }
        }

        /// <summary>
        /// Briše ličnost na osnovu identifikatora.
        /// </summary>
        /// <param name="licnostId">Identifikator licnosti (npr. 7684d0d5-2055-4a10-f724-08d9f3dcf86e)</param>
        /// <returns>string</returns>
        /// <response code="204">Vraća poruku o uspešnom brisanju</response>
        /// <response code="404">Ne postoji ličnost sa tim identifikatorom</response>
        /// <response code="500">Postoji problem sa brisanjem na serveru</response>
        [HttpDelete("{licnostId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLicnost(Guid licnostId) 
        {
            try
            {
            
                if (licnostRepository.GetLicnostById(licnostId) == null)
                {
                    return NotFound();
                }

                licnostRepository.DeleteLicnost(licnostId);
                licnostRepository.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Uspesno brisanje!");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u brisanju!");
            }
        }

        /// <summary>
        /// Menja vrednosti obeležja ličnosti.
        /// </summary>
        /// <param name="licnostDto">Model ličnosti</param>
        /// <returns>Podatke o ličnosti koja je upisana</returns>
        ///     /// <remarks>
        /// Primer zahteva za upis ličnosti \
        /// POST /api/licnost \
        /// {
        ///     "LicnostId": "8d6ab9eb-05d4-4010-6741-08d9f3bac53c",
        ///     "Ime": "Milutin",
        ///     "Prezime": "Milankovic",
        ///     "Funkcija": "Clan"
        /// }
        /// </remarks>
        /// <response code="200">Vraća podatke o izmenjenoj ličnosti</response>
        /// <response code="404">Ne postoji ličnost za koju je pokušana izmena</response>
        /// <response code="500">Postoji neki problem sa izmenom</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicnostDto> UpdateLicnost([FromBody] LicnostCUDto licnostDto)
        {
            try
            {
                Licnost staraLicnost = licnostRepository.GetLicnostById(licnostDto.LicnostId);
                if (staraLicnost == null)
                {
                    return NotFound();
                }

                Licnost licnost = mapper.Map<Licnost>(licnostDto);
                mapper.Map(licnost, staraLicnost);

                licnostRepository.SaveChanges();
                return Ok(mapper.Map<LicnostDto>(staraLicnost));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
            }
        }

        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        public IActionResult GetLicnostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
