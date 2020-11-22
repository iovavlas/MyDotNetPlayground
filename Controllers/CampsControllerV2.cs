using AutoMapper;
using Microsoft.Web.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiVersion("2.0")]                                 // API supported versions for the controller...
    [RoutePrefix("api/camps")]
    public class Camps2Controller : ApiController       // it doesn't work if we name the controller 'CampsControllerV2'...
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public Camps2Controller(ICampRepository repository, IMapper mapper)         
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET: api/Camps/5 (api-version=2.0)
        //[MapToApiVersion("2.0")]                      // We don't need this attribute, because there is only one version of this action in this controller...
        [Route("{moniker}", Name = "GetCampV2")]
        [ResponseType(typeof(object))]
        public async Task<IHttpActionResult> GetCamp(string moniker, bool includeTalks = false)
        {
            Camp result;

            try
            {
                result = await _repository.GetCampAsync(moniker, includeTalks);

                if (result == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

            // Mapping (Model/Entity --> Dto)
            CampDto mappedResult = _mapper.Map<CampDto>(result);
            return Ok(new { success = true, camp = mappedResult });
        }
    }
}
