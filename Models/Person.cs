using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    public class Person
    {
        public Person(int id, string name, int? age)    // age is nullable, but must be set...
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
        }
        public Person(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Person()     /* Demo of a parameterless constructor */ 
        { }
        
        /* Tip: 'ctor' + tab --> constructor snippet */
        /* Tip: 'prop' + tab --> new property snippet */
        /* Tip: 'propfull' + tab --> new property with a getter and setter - snippet */
        public int Id { get; set; }

        /* Demo of overriding default conventions using Data annotations. A string is nullable by default */
        [Required]
        //[Required(ErrorMessage = "Please enter person's name. It can't be empty.")]       /* Demo of a custom error message, if we want to override the default one. */
        [StringLength(255)]
        [ValidatePersonName]            // custom attribute validator
        public string Name { get; set; }

        public int? Age { get; set; }   // '?' or 'Nullable<T>' --> nullable property. 
    }
}