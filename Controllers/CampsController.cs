using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
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
        public async Task<IHttpActionResult> GetCamps()
        {
            Camp[] result; 

            try
            {
                result = await _repository.GetAllCampsAsync();
            }
            catch (Exception ex)                // Although it's a bad practice to return the exception, e.g. for safety reasons
            {
                return InternalServerError(ex);
            }

            //return Ok(result);                // It's a good practice to return a DTO (subset of our entity/model) instead of the whole entity/model.

            // Mapping (Model/Entity --> Dto)
            CampDto[] mappedResult = _mapper.Map<Camp[], CampDto[]>(result);
            return Ok(mappedResult);
        }




        /*
        // GET: api/Camps/5
        [ResponseType(typeof(Camp))]
        public IHttpActionResult GetCamp(int id)
        {
            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return NotFound();
            }

            return Ok(camp);
        }

        // PUT: api/Camps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCamp(int id, Camp camp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != camp.CampId)
            {
                return BadRequest();
            }

            db.Entry(camp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Camps
        [ResponseType(typeof(Camp))]
        public IHttpActionResult PostCamp(Camp camp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Camps.Add(camp);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = camp.CampId }, camp);
        }

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