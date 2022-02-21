using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice
{
    public class pomocna
    {


        /*
        [ApiController]
        [Route("api/prioriteti")]
        public class PrioritetController : ControllerBase
        {
            private readonly IPrioritetRepository prioritetRepository;
            private readonly LinkGenerator linkGenerator;
            private readonly IMapper mapper;

            public PrioritetController(IPrioritetRepository prioritetRepository, LinkGenerator linkGenerator, IMapper mapper)
            {
                this.prioritetRepository = prioritetRepository;
                this.linkGenerator = linkGenerator;
                this.mapper = mapper;
            }
            /// <summary>
            /// Vraca listu prioriteta koji postoje evidentirani
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            [HttpHead]
            public ActionResult<List<PrioritetModelDto>> GetPrioritetiList()
            {
                List<PrioritetModel> prioriteti = prioritetRepository.GetPrioriteti();
                if (prioriteti == null || prioriteti.Count == 0)
                {
                    return NoContent();
                }

                return Ok(mapper.Map<List<PrioritetModelDto>>(prioriteti));
            }

            [HttpGet("{prioritetId}")]
            public ActionResult<PrioritetModelDto> GetPrioritetById(Guid prioritetId)
            {
                PrioritetModel prioritetModel = prioritetRepository.GetPrioritetById(prioritetId);

                if (prioritetModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<PrioritetModelDto>(prioritetModel));
            }

            [HttpDelete("{prioritetId}")]
            public IActionResult DeletePrioritet(Guid prioritetId)
            {
                try
                {
                    PrioritetModel prioritetModel = prioritetRepository.GetPrioritetById(prioritetId);
                    if (prioritetModel == null)
                    {
                        return NotFound();
                    }
                    prioritetRepository.DeletePrioritet(prioritetId);
                    return NoContent();
                    // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                }
                catch
                {
                    tr    }

            }

            [HttpPost]
            public ActionResult<PrioritetModelDto> CreatePrioritet([FromBody] PrioritetModelDto prioritet)
            {
                Console.Write("ulaz1");
                //proveri

                try
                {
                    // Console.Write("ulaz na pocetak");
                    PrioritetModel prior = mapper.Map<PrioritetModel>(prioritet);
                    //  Console.Write("ulaz2");
                    PrioritetModel prioritetCreate = prioritetRepository.CreatePrioritet(prior);
                    //  Console.Write("ulaz3");
                    // Dobar API treba da vrati lokator gde se taj resurs nalazi
                    string location = linkGenerator.GetPathByAction("GetPrioritetById", "Prioritet", new { prioritetId = prior.PrioritetID });
                    Console.Write(location);
                    return Created(location, mapper.Map<PrioritetModel>(prioritetCreate));
                    //map u dto 

                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                }
            }

            [HttpPut]
            public ActionResult<PrioritetModelDto> UpdatePrioritet(PrioritetModelDto prioritet)
            {
                //TO-DO
                /*
                try
                {
                    //provera da li uopste postoji ovaj prioritet u pod
                    if (prioritetRepository.GetPrioritetById(prioritet.PrioritetID) == null)
                    {
                        return NotFound();
                    }
                    PrioritetModel prioritetModel = mapper.Map<PrioritetModel>(prioritet);
                    PrioritetModel conf = prioritetRepository.UpdatePrioritet(prioritetModel);
                    return Ok(mapper.Map<PrioritetModelDto>(conf));
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                } */
        /*
                return NoContent();
            }

            [HttpOptions]
            public IActionResult GetPrioritetOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
                return Ok();
            }
        */


    



        }
    }
