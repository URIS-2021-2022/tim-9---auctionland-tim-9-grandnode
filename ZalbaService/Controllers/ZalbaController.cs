using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Models;
using ZalbaService.Models.Zalba;
using ZalbaService.ServiceCalls;

namespace ZalbaService.Controllers
{
    [Route("api/zalba")]
    [ApiController]
    //[Authorize]
    public class ZalbaController : ControllerBase
    {
        private readonly IZalbaRepository zalbaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "ZalbaService";
        private Message message = new Message();
        private readonly IKupacService kupacService;

        public ZalbaController(IZalbaRepository zalbaRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IKupacService kupacService)
        {
            this.zalbaRepository = zalbaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.kupacService = kupacService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ZalbaDto>> GetAllZalba()
        {
            var zalba = zalbaRepository.GetAllZalba();
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (zalba == null || zalba.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.Information = "Returned list of Zalba";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<ZalbaDto>>(zalba));
        }

        [HttpGet("{zalbaId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ZalbaDto> GetZalba(Guid zalbaId)
        {
            var zalba = zalbaRepository.GetZalbaById(zalbaId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (zalba == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Zalba with identifier: " + zalbaId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = zalba.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ZalbaDto>(zalba));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZalbaConfirmationDto> CreateZalba([FromBody] ZalbaCreationDto zalba)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Zalba _zalba = mapper.Map<Zalba>(zalba);
                ZalbaConfirmationDto confirmation = zalbaRepository.CreateZalba(_zalba);
                zalbaRepository.SaveChanges();

                string lokacija = linkGenerator.GetPathByAction("GetZalba", "Zalba", new { zalbaId = confirmation.ZalbaId });
                message.Information = zalba.ToString() + " | Zalba location: " + lokacija;
                loggerService.CreateMessage(message);
                var kupacInfo = mapper.Map<KupacDto>(zalba);
                kupacInfo.KupacId = _zalba.PodnosilacZalbe;
                bool _licnost = kupacService.PodnosenjeZalbe(kupacInfo);
                return Created(lokacija, mapper.Map<ZalbaConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja zalbe!");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ZalbaConfirmationDto> UpdateZalba(ZalbaUpdateDto zalba)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var staraZalba = zalbaRepository.GetZalbaById(zalba.ZalbaId);
                if (staraZalba == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Zalba with identifier: " + zalba.ZalbaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Zalba novaZalba = mapper.Map<Zalba>(zalba);
                mapper.Map(novaZalba, staraZalba);
                zalbaRepository.SaveChanges();
                message.Information = staraZalba.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ZalbaConfirmationDto>(staraZalba));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene zalbe!");
            }
        }

        [HttpDelete("{zalbaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteZalba(Guid zalbaId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                var zalba = zalbaRepository.GetZalbaById(zalbaId);
                if (zalba == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Zalba with identifier: " + zalbaId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                zalbaRepository.DeleteZalba(zalbaId);
                zalbaRepository.SaveChanges();
                message.Information = "Successfully deleted " + zalba.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + zalba.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja zalbe!");
            }
        }
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetZalbaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
