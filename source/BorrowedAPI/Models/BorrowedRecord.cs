using System.ComponentModel.DataAnnotations;

namespace BorrowedAPI.Models
{
    public class BorrowedRecord
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public Book Book { get; set; } = null!;

        [Required]
        public int VisitorId { get; set; }

        [Required]
        public Visitor Visitor { get; set; } = null!;

        [Required]
        public DateTime BorrowedDate { get; set; }

        [Required]
        public DateTime ReturnDeadline { get; set; }
    }
}
