using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Person
    {
        public Person(int Id, string Name, int? Age)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
        }

        /* Tip: 'prop' + tab --> new property snippet */
        public int Id { get; set; }

        /* Demo of overriding default conventions using Data annotations. A string is by default nullable */
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int? Age { get; set; }   // '?' --> optional/nullable property
    }
}