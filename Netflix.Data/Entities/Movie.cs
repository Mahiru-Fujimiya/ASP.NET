using System.ComponentModel.DataAnnotations;

namespace Netflix.Data.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; } // [cite: 263]
        [Required, StringLength(255)]
        public string Title { get; set; } // [cite: 263]
        public string Description { get; set; } // [cite: 263]
        public string Poster { get; set; } // [cite: 263]

        // Liên kết với Genre và Episode
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Episode> Episodes { get; set; } // [cite: 143, 237, 279]
    }
}