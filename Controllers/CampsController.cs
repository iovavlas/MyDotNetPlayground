using AutoMapper;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository repository, IMapper mapper)          // DI by using Autofac... 
        {
            _repository = repository;
            _mapper = mapper;
        }



        // GET: api/Camps
        // GET: api/Camps?includeTalks=true     // Query Strings should always be optional.
        [Route()]                               // the URI pattern is here empty, because we use the [RoutePrefix] attribute for the whole Controller.
        public async Task<IHttpActionResult> GetCamps(bool includeTalks = false)
        {
            Camp[] result;

            try
            {
                result = await _repository.GetAllCampsAsync(includeTalks);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Although it's a bad practice to return the exception, e.g. we may not want to reveil some information for safety reasons.
            }

            //return Ok(result);                // It's a good practice to return a DTO (subset of our entity/model) instead of the whole entity/model.

            // Mapping (Model/Entity --> Dto)
            CampDto[] mappedResult = _mapper.Map<Camp[], CampDto[]>(result);
            return Ok(mappedResult);
        }


        // GET: api/Camps/5
        [Route("{moniker}", Name = "GetCamp")]
        [ResponseType(typeof(CampDto))]         // useful when returning an IHttpActionResult...
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
            return Ok(mappedResult);
        }


        // GET: api/Camps/searchByDate?eventDate=2018-10-18&includeTalks=true           // eventDate is here optional...
        //[Route("searchByDate")] 
        //      or ... 
        // GET: api/Camps/searchByDate/2018-10-18&includeTalks=true                     // eventDate is here required and must be part of the [Route()]...
        [Route("searchByDate/{eventDate:datetime}")]
        [HttpGet]
        [ResponseType(typeof(CampDto[]))]
        public async Task<IHttpActionResult> SearchCampsByEventDate(DateTime eventDate, bool includeTalks = false)
        {
            Camp[] result;

            try
            {
                result = await _repository.GetAllCampsByEventDate(eventDate, includeTalks);             // on searching we shouldn't return NotFound. An empty Collection is fine...
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            CampDto[] mappedResult = _mapper.Map<CampDto[]>(result);
            return Ok(mappedResult);
        }


        // POST: api/Camps
        [Route()]
        [HttpPost]
        [ResponseType(typeof(CampDto))]
        public async Task<IHttpActionResult> CreateCamp(CampDto campDto)
        {
            try
            {
                if (await _repository.GetCampAsync(campDto.Moniker) != null)
                {
                    ModelState.AddModelError("Moniker", "Moniker is already in use / not unique!");
                }

                if (ModelState.IsValid)                                             /* validate the model, according to the data annotation (e.g. [Required]) defined in the CampDto class. */
                {
                    Camp camp = _mapper.Map<Camp>(campDto);

                    _repository.AddCamp(camp);

                    if (await _repository.SaveChangesAsync())
                    {
                        var newCamp = _mapper.Map<CampDto>(camp);                   // 'camp' here, after saving, includes an id generated from the DB...

                        //return Created(new Uri(Request.RequestUri + "/" + newCamp.Moniker), newCamp);
                        return CreatedAtRoute("GetCamp", new { moniker = newCamp.Moniker }, newCamp);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }


        // PUT: api/Camps/5
        [Route("{moniker}")]
        [HttpPut]
        [ResponseType(typeof(CampDto))]
        public async Task<IHttpActionResult> UpdateCamp(string moniker, CampDto campDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (moniker != campDto.Moniker)
            {
                return BadRequest("moniker != Moniker ==> Confusion of da highest order!!! :)");
            }

            try
            {
                Camp camp = await _repository.GetCampAsync(moniker); 

                if (camp == null)
                {
                    return NotFound();
                }

                _mapper.Map(campDto, camp);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<CampDto>(camp));
                    //return StatusCode(HttpStatusCode.NoContent);
                } 
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        /*
        // DELETE: api/Camps/5
        [ResponseType(typeof(Camp))]
        public IHttpActionResult DeleteCamp(int id)
        {
            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return NotFound();
            }

            db.Camps.Remove(camp);
            db.SaveChanges();

            return Ok(camp);
        }
        */
    }
}
