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
    [Route("api/tipKorisnika")]

    public class TipKorisnikaController : ControllerBase
    {

        private readonly ITipKorisnikaRepository tipKorisnikaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private Message message = new Message();
        public TipKorisnikaController(ITipKorisnikaRepository tipRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.tipKorisnikaRepository = tipRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        public ActionResult<List<TipKorisnikaDto>> GetTipKorisnikaList()
        {
            List<TipKorisnika> tipovi = tipKorisnikaRepository.GetTipKorisnikaList();
            message.Method = "GET";


            if (tipovi == null || tipovi.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of types";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<TipKorisnikaDto>>(tipovi));
        }


        [HttpGet("{tipKorisnikaId}")]
        public ActionResult<TipKorisnikaDto> GetTipKorisnikaById(Guid tipKorisnikaId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            TipKorisnika tipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnikaId);
            message.Method = "GET";

            if (tipKorisnika == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of type with identifier: " + tipKorisnikaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = tipKorisnika.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<TipKorisnikaDto>(tipKorisnika));
        }

        [HttpPost]
        public ActionResult<TipKorisnikaDto> CreateTipKorisnika([FromBody] TipKorisnikaDto tipKorisnika)
        {
            message.Method = "POST";

            try
            {

                TipKorisnika createTip = mapper.Map<TipKorisnika>(tipKorisnika);
                TipKorisnika confirmation = tipKorisnikaRepository.CreateTipKorisnika(createTip);

                string location = linkGenerator.GetPathByAction("GetDrzavaList", "Drzava", new { tipKorisnikaId = confirmation.tipKorisnikaId });

                message.Information = createTip.ToString();
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<TipKorisnikaDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{tipKorisnikaId}")]
        public IActionResult DeleteTipKorisnika(Guid tipKorisnikaId)
        {
            message.Method = "DELETE";

            try
            {
                TipKorisnika tipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnikaId);
                if (tipKorisnika == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of type with identifier: " + tipKorisnikaId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }

                tipKorisnikaRepository.DeleteTipKorisnika(tipKorisnikaId);

                tipKorisnikaRepository.SaveChanges();
                message.Information = "Successfully deleted " + tipKorisnika.ToString();

                return NoContent();
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error during the deletion");
            }
        }

        [HttpPut]
        public ActionResult<TipKorisnikaDto> UpdateTipKorisnika(TipKorisnika tipKorisnika)
        {
            message.Method = "PUT";

            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var stariTipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnika.tipKorisnikaId);
                if (stariTipKorisnika == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of type with identifier: " + tipKorisnika.tipKorisnikaId;
                    loggerService.CreateMessage(message);

                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                TipKorisnika tipKorisnikaEntity = mapper.Map<TipKorisnika>(tipKorisnika);

                mapper.Map(tipKorisnikaEntity, stariTipKorisnika); //Update objekta koji treba da sačuvamo u bazi                

                tipKorisnikaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = tipKorisnikaEntity.ToString();
                loggerService.CreateMessage(message);

                tipKorisnikaRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<TipKorisnikaDto>(stariTipKorisnika));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        [HttpOptions]
        public IActionResult GetTipKorisnikaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }


    }
}
