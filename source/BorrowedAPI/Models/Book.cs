using System.ComponentModel.DataAnnotations;

namespace BorrowedAPI.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public List<BorrowedRecord> BorrowedHistory { get; set; } = new List<BorrowedRecord>();
    }
}
