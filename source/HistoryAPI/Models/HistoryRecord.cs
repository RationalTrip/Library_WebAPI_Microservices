using System.ComponentModel.DataAnnotations;

namespace HistoryAPI.Models
{
    public class HistoryRecord
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
        public DateTime ReturnedDate { get; set; }

        [Required]
        public bool IsReturnedLate { get; set; }
    }
}
