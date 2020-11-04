using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class MoviesController : ApiController
    {
        // GET: api/movies/released/1990/02
        [HttpGet]
        [Route("api/movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]    /* Demo of a custom Route / attribute routing with constraints */
        // Does it also work with a query string? e.g. api/movies/released?year=1990&month=02 --> For that I must configure the route accordingly... 
        public string ByReleaseYear(int year, int month)
        {
            return String.Format("year:{0}, month:{1}", year, CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month));
        }
    }
}
