using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public static class Database
    {
        private static List<Person> persons;

        public static List<Person> Persons
        {
            get
            {
                if (persons == null)
                {
                    persons = new List<Person>();
                    persons.Add(new Person(1, "Name1", 30));
                    persons.Add(new Person { Id = 4, Name = "Name4" });     // Why do I get a compile error? --> I need a parameterless constructor for that. The 3rd one.
                    persons.Add(new Person(5, "Name5") { Age = 25 });
                    persons.Add(new Person(2, "Name2"));
                    persons.Add(new Person(3, "Name3", 'c'));               // Why don't I get a compile error, since 'Age' should be an int? --> implicit cast 
                }
                return persons;
            }
            set
            {                
                persons = value;
            }
        }

    }
}