namespace WebApplication1.Models
{
    public class TalkDto
    {
        public int TalkId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }


        // include Speaker Data...
        public SpeakerDto Speaker { get; set; }
    }
}