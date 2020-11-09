using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 255)
                .Must(name => name == "<dead>")
                .When(m => m.Age > 99);
        }
    }
}