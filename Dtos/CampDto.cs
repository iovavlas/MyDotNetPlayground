using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CampDto
    {
        public string Name { get; set; }
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        public int Length { get; set; } = 1;


        // include Location Data...
        //public string LocationVenueName { get; set; }             // using the 'Location' prefix, we don't need to configure anything. Automapper recognises that automatically.
        public string Venue { get; set; }                           // If we don't want to use the 'Location' prefix, we must adjust the mapping profile. 
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }


        // include Talk Data...
        public ICollection<TalkDto> Talks { get; set; }
    }
}