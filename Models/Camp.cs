using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Camp
    {
        public int CampId { get; set; }                     // In EF every entity(model) must have a key (as primary key), which should be named either Id or {Type}Id
        [Required]                                          // Demo of overriding default conventions using Data annotations. A string is nullable by default 
        public string Name { get; set; }
        [Required]
        public string Moniker { get; set; }
        public Location Location { get; set; }              // navigation property from one model to another.
        //public int LocationId { get; set; }               // If we don't want to load the whole Location entity, we could just load the LocationId, which will be recognised from EF as a foreign key. 
        [Required]
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        [Required]
        [Range(1, 30)]
        public int Length { get; set; } = 1;
        public ICollection<Talk> Talks { get; set; }
    }
}