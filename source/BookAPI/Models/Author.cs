using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string SecondName { get; set; } = string.Empty;

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
