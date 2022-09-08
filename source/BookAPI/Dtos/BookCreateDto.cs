using System.ComponentModel.DataAnnotations;

namespace BookAPI.Dtos
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime PublicationDate { get; set; }

        public string? About { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
