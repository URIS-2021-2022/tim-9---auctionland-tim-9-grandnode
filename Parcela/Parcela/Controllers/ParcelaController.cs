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
using Parcela.ServiceCalls;

namespace Parcela.Controllers
{
    [ApiController]
    [Route("api/parcela")]
    [Produces("application/json")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository parcelaRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly IKupac_SKService kupac_SKService;

        private readonly ILoggerService loggerService;
        private readonly string serviceName = "Parcela";
        private Message message = new Message();


        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKupac_SKService kupac_SKService)
        {
            this.parcelaRepository = parcelaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.kupac_SKService = kupac_SKService;

        }


        [HttpGet]
        [HttpHead]
        public ActionResult<List<ParcelaDto>> GetParcelasList()
        {
            List<Parcela.Entities.Parcela> parcelaLista = parcelaRepository.GetParcelaList();


            message.ServiceName = serviceName;
            message.Method = "GET";
            if (parcelaLista == null || parcelaLista.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            try
            {
                foreach (Parcela.Entities.Parcela p in parcelaLista)
                {
                    KupacDto kupac = kupac_SKService.GetKupacById(p.KorisnikParceleID).Result;
                    if (kupac != null)
                    {
                        p.KupacDto = kupac;
                    }
                }
            }
            catch
            {
                return default;
            }
            message.Information = "Returned list of Parcela";
            loggerService.CreateMessage(message);

            return Ok(mapper.Map<List<ParcelaDto>>(parcelaLista));
        }

        [HttpGet("{parcelaId}")]
        public ActionResult<ParcelaDto> GetParcelaById(Guid parcelaId)
        {
            Parcela.Entities.Parcela parcelaModel = parcelaRepository.GetParcelaById(parcelaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (parcelaModel == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Parcela with identifier: " + parcelaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = parcelaModel.ToString();
            loggerService.CreateMessage(message);

            return Ok(mapper.Map<ParcelaDto>(parcelaModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<ParcelaDto> CreateParcela([FromBody] ParcelaDto parcela)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";

            try
            {

                Parcela.Entities.Parcela p = mapper.Map<Parcela.Entities.Parcela>(parcela);
                Parcela.Entities.Parcela confirmation = parcelaRepository.CreateParcela(p);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                
                string location = linkGenerator.GetPathByAction("GeParcelaById", "Parcela", new { parcelaID = confirmation.ParcelaID });

                message.Information = parcela.ToString() + " | Parcela location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<ParcelaDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{parcelaId}")]
        public IActionResult DeleteParcela(Guid parcelaId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                Parcela.Entities.Parcela p = parcelaRepository.GetParcelaById(parcelaId);
                if (p == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Parcela with identifier: " + parcelaId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }


                parcelaRepository.DeleteParcela(parcelaId);
                parcelaRepository.SaveChanges();

                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                message.Information = "Successfully deleted " + p.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + p.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Deletion error!");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelaDto> UpdateParcela(ParcelaDto parcela)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldParcela = parcelaRepository.GetParcelaById(parcela.ParcelaID);
                if (oldParcela == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Parcela with identifier: " + parcela.ParcelaID;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
               Parcela.Entities.Parcela newParcela = mapper.Map<Parcela.Entities.Parcela>(parcela);

                mapper.Map(newParcela,oldParcela) ; //Update objekta koji treba da sačuvamo u bazi                

                parcelaRepository.SaveChanges(); //Perzistiramo promene
                message.Information = oldParcela.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ParcelaDto>(oldParcela));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska u izmeni");
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
