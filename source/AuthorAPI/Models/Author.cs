using System.ComponentModel.DataAnnotations;

namespace AuthorAPI.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string SecondName { get; set; } = null!;

        [Required]
        public DateTime Birthday { get; set; }

        public DateTime? DeathDate { get; set; }

        public string? About { get; set; }
    }
}
