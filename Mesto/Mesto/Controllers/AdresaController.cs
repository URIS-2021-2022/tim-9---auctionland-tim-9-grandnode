using AutoMapper;
using Mesto.Data;
using Mesto.Entities;
using Mesto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Controllers
{
    [ApiController]
    [Route("api/adresa")]
    public class AdresaController : ControllerBase
    {
        private readonly IAdresaRepository adresaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public AdresaController(IAdresaRepository adresaRepository, LinkGenerator linkGenerator,IMapper mapper)
        {
            this.adresaRepository = adresaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AdresaDto>> GetAdresaList()
        {
            var adrese = adresaRepository.GetAdresaList();


            if (adrese == null || adrese.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AdresaDto>>(adrese));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{adresaId}")]
        public ActionResult<AdresaDto> GetAdresaById(Guid adresaId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var adresa = adresaRepository.GetAdresaById(adresaId);

            if (adresa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AdresaDto>(adresa));
        }

        [HttpPost]
        public ActionResult<AdresaDto> CreateAdresa([FromBody] AdresaDto adresa)
        {
            try
            {

                Adresa a = mapper.Map<Adresa>(adresa);
                Adresa adresa1 = adresaRepository.CreateAdresa(a);

                string location = linkGenerator.GetPathByAction("GetAdresaList", "Adresa", new { adresaId = adresa1.AdresaId });
                return Created(location, mapper.Map<AdresaDto>(adresa1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{adresaId}")]
        public IActionResult DeleteAdresa(Guid adresaId)
        {
            try
            {
                Adresa adresa = adresaRepository.GetAdresaById(adresaId);
                if (adresa == null)
                {
                    return NotFound();
                }

                adresaRepository.DeleteAdresa(adresaId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja adrese!");
            }
        }

        //[HttpPut]
        //public ActionResult<AdresaDto> UpdateAdresa(Adresa adresa)
        //{
        //    try
        //    {
        //        if (adresaRepository.GetAdresaById(adresa.AdresaId) == null)
        //        {
        //            return NotFound();
        //        }

        //        Adresa a = adresaRepository.UpdateAdresa(adresa);

        //        return Ok(mapper.Map<AdresaDto>(a));
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja adrese!");
        //    }
        //}
        [HttpOptions]
        public IActionResult GetAdresaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }

    }
    }

