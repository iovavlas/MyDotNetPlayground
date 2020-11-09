using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonController : ApiController
    {
        // GET: api/Person
        public IEnumerable<Person> GetAllPersons()
        {
            var result = Database.Persons;

            //result = (List<Person>)persons.OrderBy(person => person.Id);       // We get a casting error, if we don't call the 'ToList()' method.
            result = Database.Persons.OrderBy(person => person.Id).ToList();

            //Console.WriteLine("test");        // To see the output, we must first attach to process. No need to select 'w3wp' when using IIS Express. For a better logger see Serilog.net ...
            //Trace.WriteLine("test");
            Debug.WriteLine("3rd Person has an age of: {0}", result[2].Age);

            foreach (var person in result)
            {
                if (!person.Age.HasValue) { /* Demo of HasValue */ }
                if (string.IsNullOrWhiteSpace(person.Name)) { /* Demo of IsNullOrWhiteSpace */ }
                string str = string.Format("person id:{0}, name:{1}", person.Id, person.Name);  /* Demo of string interpolation */
            }

            return result;
        }


        // GET: api/Person/5
        public Person GetPerson(int id)
        {
            //var result = persons.Find(person => person.Id == id);
            var result = Database.Persons.SingleOrDefault(person => person.Id == id);       // 'SingleOrDefault' does the same thing as 'Find'...

            if (result == null)         // In FC we leave this check to the client
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return result;
        }

        /*
        // POST: api/Person
        public HttpResponseMessage Post(Person person)              // Demo of returning a HttpResponseMessage, instead of the newly created item or nothing (void).
        {                                                           // This approach returns the ModelState (e.g. form errors) to the client...
            if (ModelState.IsValid)     
            {
                // insert the person in the DB (not shown)...

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        */

        
        // POST: api/Person
        [HttpPost]                          // We need this attribute, because the method name doesn't start with the word 'Post'.
        //[ValidatePersonName]              // It won't get triggered. Why? --> Because it's an attribute validator, not a model validator. We use it inside the model class. 
        [NotNullValidation]                 // custom model validator, to check if the request body (person) is empty.
        //[ValidateInputModel(Validator = typeof(PersonValidator))]   // custom model validator using FluentValidation.   TODO: Not working?
        [InputValidation(ValidatorType = typeof(PersonValidator))]
        public Person CreatePerson(Person person)
        {
            if (!ModelState.IsValid)     /* validate the model, according to the data annotation (e.g. [Required]) defined in the model class. 
                                          * If the request is empty (person->null), ModelState.IsValid is nonetheless true..! That's why we check that with an extra validator (NotNullValidation). */
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // insert the person in the DB (not shown). Add the person to the list instead...
            //int generatedId = (Database.Persons.OrderByDescending(v => v.Id).First()).Id + 1;
            int generatedId = Database.Persons.Max(x => x.Id) + 1;
            person.Id = generatedId;
            Database.Persons.Add(person);               // The new person addition won't get persisted. Why is that? --> The constructor gets executed after every api call. We need a singleton for that...

            return person;
        }


        // POST: api/Person/v2
        [HttpPost]
        [Route("api/Person/v2")]            // We need this attribute, because we have 2 POST methods with the same parameters at the same end point.
        public IHttpActionResult CreatePersonRestfully(Person person)           // Demo of a more RESTful API approach, returning a 201 Created, the location of the new item(resource) and the new item(resource) itself, instead of 200 OK and just the new item.
        {
            if (!ModelState.IsValid)     
            {
                return BadRequest();
            }

            // insert the person in the DB (not shown). Add the person to the list instead...
            int generatedId = (Database.Persons.OrderByDescending(v => v.Id).First()).Id + 1;
            person.Id = generatedId;
            Database.Persons.Add(person);               

            return Created(new Uri(Request.RequestUri + "/" + generatedId), person);
        }


        // PUT: api/Person/5
        [HttpPut]
        /*  If you have:
            - a primitive type in the URI, or
            - a complex type in the body
            then you don't have to add any attributes (neither [FromBody] nor [FromUri]).
         */
        // TODO: Why do we use in FC the POST Verb to update a resource, instead of PUT? 
        // Is it safer? How do we then determine, if we need to update or insert a resource? 
        public void UpdatePerson(int id, Person person)
        {
            if (!ModelState.IsValid)     
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // update the person in the DB (not shown). We do it in the list instead...
            var personInDb = Database.Persons.SingleOrDefault(item => item.Id == id);

            if (personInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if (personInDb.Id != id)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            personInDb = person;    // TODO: DeepClone() ? 
        }


        // DELETE: api/Person/5
        public void Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // delete the person from the DB (not shown). We delete it from the list instead...
            var personInDb = Database.Persons.SingleOrDefault(item => item.Id == id);

            if (personInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if (personInDb.Id != id)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Database.Persons.Remove(personInDb);
        }
    }
}
