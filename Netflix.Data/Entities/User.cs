using System.ComponentModel.DataAnnotations;

namespace Netflix.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; } // [cite: 261]
        [Required, StringLength(100)]
        public string Username { get; set; } // [cite: 261]
        [Required, EmailAddress]
        public string Email { get; set; } // [cite: 261]
        [Required]
        public string PasswordHash { get; set; } // [cite: 261, 334]
        public string Role { get; set; } = "USER"; // [cite: 249, 250, 337]
    }
}