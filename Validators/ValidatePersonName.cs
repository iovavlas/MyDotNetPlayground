using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public sealed class ValidatePersonName : ValidationAttribute        // class must be sealed
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var person = (Person)validationContext.ObjectInstance;

            if (person.Name.Any(char.IsDigit))
            {
                return new ValidationResult("Person's name can't contain any numbers");
            }

            return ValidationResult.Success;
        }
    }
}