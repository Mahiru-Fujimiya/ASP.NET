using System.ComponentModel.DataAnnotations;

namespace Netflix.Data.Entities
{
    public class Episode
    {
        [Key]
        public int Id { get; set; } // [cite: 267]
        public int MovieId { get; set; } // [cite: 267]
        public int EpisodeNumber { get; set; } // [cite: 267]
        [Required]
        public string VideoUrl { get; set; } // [cite: 267, 498]

        public Movie Movie { get; set; }
    }
}