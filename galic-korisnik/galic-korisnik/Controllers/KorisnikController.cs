using galic_korisnik.Data;
using galic_korisnik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Controllers
{
    [ApiController]
    [Route("api/korisnici")]
    public class KorisnikController : ControllerBase
    {
        //dependency injector
        private readonly IKorisnikRepository korisnikRepository;

        public KorisnikController(IKorisnikRepository korisnikRepository)
        {
            this.korisnikRepository = korisnikRepository;
        }

        [HttpGet]
        public ActionResult<List<KorisnikModel>> GetKorisnikList()
        {
            List<KorisnikModel> korisnikList = korisnikRepository.GetKorisnikList();
            if (korisnikList == null || korisnikList.Count == 0)
            {
                return NoContent();
            }
            return Ok(korisnikList);
        }

        [HttpGet("{korisnikId}")]
        public ActionResult<List<KorisnikModel>> GetKorisnikById(Guid korisnikId)
        {
            KorisnikModel korisnik = korisnikRepository.GetKorisnikById(korisnikId);

            if (korisnik == null)
            {
                return NotFound();
            }
            return Ok(korisnik);
        }

        [HttpPost]
        public ActionResult<KorisnikModel> CreateKorisnik([FromBody] KorisnikModel korisnik) //FromBody uzima iz bodya requesta
        {
            try
            {
                KorisnikModel createKorisnik = new KorisnikModel();
                createKorisnik = korisnikRepository.CreateKorisnik(korisnik);

                return createKorisnik;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{KorisnikId}")]
        public IActionResult DeleteKorisnik(Guid korisnikId)
        {
            try
            {
                KorisnikModel deleteKorisnik = korisnikRepository.GetKorisnikById(korisnikId);
                if (deleteKorisnik == null)
                {
                    return NotFound();
                }
                korisnikRepository.DeleteKorisnik(korisnikId);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        [HttpPut]
        public ActionResult<KorisnikModel> UpdateKorisnik(KorisnikModel korisnik)
        {
            try
            {
                //Proveriti da li postoji korisnik
                if (korisnikRepository.GetKorisnikById(korisnik.korisnikId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ExamRegistration examRegistrationEntity = mapper.Map<ExamRegistration>(examRegistration);
                ExamRegistrationConfirmation confirmation = examRegistrationRepository.UpdateExamRegistration(examRegistrationEntity);
                return Ok(mapper.Map<ExamRegistrationConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        */
    }
}
