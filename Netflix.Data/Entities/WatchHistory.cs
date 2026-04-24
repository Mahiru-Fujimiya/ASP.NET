using System.ComponentModel.DataAnnotations;

namespace Netflix.Data.Entities
{
    public class WatchHistory
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime WatchedTime { get; set; } // [cite: 271]
        public DateTime WatchedAt { get; set; } = DateTime.Now;

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}