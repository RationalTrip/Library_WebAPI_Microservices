using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime PublicationDate { get; set; }

        public string? About { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public Author Author { get; set; } = null!;
    }
}
