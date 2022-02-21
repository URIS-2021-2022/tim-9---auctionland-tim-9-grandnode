using AutoMapper;
using galic_korisnik.Data;
using galic_korisnik.Entities;
using galic_korisnik.Models;
using galic_korisnik.ServiceCalls;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;
        private Message message = new Message();

        public KorisnikController(IKorisnikRepository korisnikRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
        }

        [HttpGet]
        public ActionResult<List<KorisnikDto>> GetKorisnikList()
        {
            List<Korisnik> korisnikList = korisnikRepository.GetKorisnikList();
            message.Method = "GET";

            if (korisnikList == null || korisnikList.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of users!";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<KorisnikDto>>(korisnikList));
        }

        [HttpGet("{korisnikId}")]
        public ActionResult<List<KorisnikDto>> GetKorisnikById(Guid korisnikId)
        {
            Korisnik korisnik = korisnikRepository.GetKorisnikById(korisnikId);
            message.Method = "GET";

            if (korisnik == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of user with identifier: " + korisnikId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = korisnik.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<KorisnikDto>(korisnik));
        }

        [HttpPost]
        public ActionResult<KorisnikDto> CreateKorisnik([FromBody] KorisnikCreateDto korisnik) //FromBody uzima iz bodya requesta
        {
            message.Method = "POST";

            try
            {
                Korisnik createKorisnik = mapper.Map<Korisnik>(korisnik);
                Korisnik confirmation = korisnikRepository.CreateKorisnik(createKorisnik);

                string location = linkGenerator.GetPathByAction("GetKorisnik", "Korisnik", new { korisnikId = confirmation.korisnikId });

                message.Information = createKorisnik.ToString();
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<KorisnikDto>(confirmation));
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation Error");
            }
        }

        [HttpDelete("{korisnikId}")]
        public IActionResult DeleteKorisnik(Guid korisnikId)
        {
            message.Method = "DELETE";

            try
            {
                Korisnik deleteKorisnik = korisnikRepository.GetKorisnikById(korisnikId);
                if (deleteKorisnik == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of user with identifier: " + korisnikId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }
                korisnikRepository.DeleteKorisnik(korisnikId);

                korisnikRepository.SaveChanges();
                message.Information = "Successfully deleted " + deleteKorisnik.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + deleteKorisnik.ToString());
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        [HttpPut]
        public ActionResult<KorisnikDto> UpdateKorisnik(KorisnikUpdateDto korisnik)
        {
            message.Method = "PUT";

            try
            {
                Korisnik stariKorisnik = korisnikRepository.GetKorisnikById(korisnik.korisnikId);

                //Proveriti da li postoji korisnik
                if (stariKorisnik == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of user with identifier: " + korisnik.korisnikId;
                    loggerService.CreateMessage(message);

                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                Korisnik updateKorisnik = mapper.Map<Korisnik>(korisnik);

                mapper.Map(korisnik, stariKorisnik);

                korisnikRepository.SaveChanges();
                message.Information = updateKorisnik.ToString();
                loggerService.CreateMessage(message);

                return Ok(mapper.Map<Korisnik>(updateKorisnik));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        
        public IActionResult GetKorisnikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            return Ok();
        }
    }
}
