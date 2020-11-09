using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public sealed class PersonNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var person = (Person)validationContext.ObjectInstance;

            if(person == null)
            {
                return new ValidationResult("Person could not be null!");
            }

            if (person.Name.Any(char.IsDigit))
            {
                return new ValidationResult("Person name can not contain any number!");
            }

            return ValidationResult.Success;            
        }
    }
}