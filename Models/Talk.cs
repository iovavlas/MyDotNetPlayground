using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Talk
    {
        public int TalkId { get; set; }
        public Camp Camp { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(4096, MinimumLength = 10)]
        public string Abstract { get; set; }
        [Required]
        [Range(100, 900)]
        public int Level { get; set; }
        public Speaker Speaker { get; set; }
    }
}