using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TalkDto
    {
        public int TalkId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(4096, MinimumLength = 10)]
        public string Abstract { get; set; }
        [Required]
        [Range(100, 900)]
        public int Level { get; set; }


        // include Speaker Data...
        public SpeakerDto Speaker { get; set; }
    }
}