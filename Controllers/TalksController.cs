using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/camps/{moniker}/talks")]
    public class TalksController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public TalksController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET: api/Camps/ATL2018/talks
        // GET: api/Camps/ATL2018/talks?includeSpeakers=true            // Query Strings should always be optional.
        [Route()]                                                       // the URI pattern is here empty, because we use the [RoutePrefix] attribute for the whole Controller.
        [HttpGet]                                                       // unnecessary here...
        [ResponseType(typeof(TalkDto[]))]                               // helpful for creating RESTful Web APIs and also when autogenerating documentation via Swagger.
        public async Task<IHttpActionResult> Get(string moniker, bool includeSpeakers = false)
        {
            try
            {
                Talk[] talks = await _repository.GetTalksByMonikerAsync(moniker, includeSpeakers);
                
                if (talks == null || talks.Length == 0)
                {
                    return NotFound();
                }

                //TalkDto[] mappedTalks = _mapper.Map<Talk[], TalkDto[]>(talks);
                var mappedTalks = _mapper.Map<IEnumerable<TalkDto>>(talks);
                return Ok(mappedTalks);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
