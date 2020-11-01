using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
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
        // GET: api/Person
        public IEnumerable<Person> Get()
        {
            var result = new List<Person>();
            result.Add(new Person(1, "Name1", 30));
            result.Add(new Person(2, "Name2", null));
            result.Add(new Person(3, "Name3", 'c'));        // TODO: Why don't I get a compile error, since 'Age' should be an int?

            //result = (List<Person>)result.OrderBy(person => person.Id);   --> TODO: Casting error...

            //Console.WriteLine("test");    --> TODO: Where is the fucking Output ? 
            //Trace.WriteLine("test");
            //Debug.WriteLine("test");

            foreach (var person in result)
            {
                if (!person.Age.HasValue) { /* Demo of HasValue */ }
                if (String.IsNullOrWhiteSpace(person.Name)) { /* Demo of IsNullOrWhiteSpace */ }
                string strInterpol = String.Format("person id:{0}, name:{1}", person.Id, person.Name);  /* Demo of string interpolation */
            }

            //return HttpNotFoundResult();  --> TODO: Compile Error
            
            return result;
        }


        /*
        // GET: api/Person/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Person
        public void Post([FromBody]string value)
        {
        }

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
