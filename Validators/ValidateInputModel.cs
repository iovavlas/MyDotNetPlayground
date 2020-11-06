using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    public sealed class ValidateInputModel : Attribute
    {
        public Type Validator { get; set; }
    }
}