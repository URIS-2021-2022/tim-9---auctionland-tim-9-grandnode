using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Parcela.Data;
using Parcela.Models;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/parcela")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.parcelaRepository = parcelaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ParcelaDto>> GetParcelasList()
        {
            List<Parcela.Entities.Parcela> parcelaLista = parcelaRepository.GetParcelaList();
            if (parcelaLista == null || parcelaLista.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ParcelaDto>>(parcelaLista));
        }

        [HttpGet("{parcelaId}")]
        public ActionResult<ParcelaDto> GetParcelaById(Guid parcelaId)
        {
            Parcela.Entities.Parcela parcelaModel = parcelaRepository.GetParcelaById(parcelaId);
            if (parcelaModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ParcelaDto>(parcelaModel));
        }

        [HttpPost]
        public ActionResult<ParcelaConfrimationDto> CreateParcela([FromBody] ParcelaConfrimationDto parcela)
        {
            try
            {

               Parcela.Entities.Parcela p = mapper.Map<Parcela.Entities.Parcela>(parcela);

                ParcelaConfrimation confirmation = parcelaRepository.CreateParcela(p);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GeParcelaById", "Parcela", new { parcelaID = confirmation.ParcelaID });
                return Created(location, mapper.Map<ParcelaConfrimationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{parcelaId}")]
        public IActionResult DeleteParcela(Guid parcelaId)
        {
            try
            {
                Parcela.Entities.Parcela parcelaModel = parcelaRepository.GetParcelaById(parcelaId);
                if (parcelaId == null)
                {
                    return NotFound();
                }
                parcelaRepository.DeleteParcela(parcelaId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<ParcelaConfrimationDto> UpdateParcela(ParcelaUpdateDto parcela)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (parcelaRepository.GetParcelaById(parcela.ParcelaID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Parcela.Entities.Parcela p = mapper.Map<Parcela.Entities.Parcela>(parcela);
                ParcelaConfrimation confirmation = parcelaRepository.UpdateParcela(p);
                return Ok(mapper.Map<ParceleConfrimationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetParcelaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
