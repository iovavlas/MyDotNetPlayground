using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonController : ApiController
    {
        public List<Person> persons { get; private set; }

        public PersonController()
        {
            persons = new List<Person>();
            persons.Add(new Person(1, "Name1", 30));
            persons.Add(new Person { Id = 4, Name = "Name4" });     // Why do I get a compile error? --> I need a parameterless constructor for that. The 3rd one.
            persons.Add(new Person(5, "Name5") { Age = 25 });     
            persons.Add(new Person(2, "Name2"));
            persons.Add(new Person(3, "Name3", 'c'));               // Why don't I get a compile error, since 'Age' should be an int? --> implicit cast 
        }


        // GET: api/Person
        public IEnumerable<Person> Get()
        {
            var result = this.persons;

            //result = (List<Person>)persons.OrderBy(person => person.Id);       // We get a casting error, if we don't call the 'ToList()' method.
            result = persons.OrderBy(person => person.Id).ToList();    

            //Console.WriteLine("test");        // To see the output, we must first attach to process. No need to select 'w3wp' hier. For a better logger see Serilog.net ...
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
        public Person Get(int id)
        {
            var result = persons.Find(person => person.Id == id);
            return result;
        }

        // POST: api/Person
        //[ValidatePersonName]              // It won't get triggered. Why? --> Because it's an attribute validator, not a model validator. 
        [NotNullValidation]                 // custom model validator, to check if the request body (person) is empty.
        [ValidateInputModel(Validator = typeof(PersonValidator))]   // custom model validator using FluentValidation.   TODO: Not working?
        public HttpResponseMessage Post(Person person)
        {
            if (ModelState.IsValid)     /* validate the model, according to the data annotation (e.g. [Required]) defined in the model class. 
                                         * If the request is empty (person->null), ModelState.IsValid is nonetheless true..! That's why we check that with an extra validator (NotNullValidation). */
            {
                // insert the person in the DB (not shown)...

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /*
        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
        */
    }
}
