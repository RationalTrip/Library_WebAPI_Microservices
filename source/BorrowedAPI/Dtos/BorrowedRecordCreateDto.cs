using System.ComponentModel.DataAnnotations;

namespace BorrowedAPI.Dtos
{
    public class BorrowedRecordCreateDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int VisitorId { get; set; }

        [Required]
        public DateTime BorrowedDate { get; set; }

        [Required]
        public DateTime ReturnDeadline { get; set; }
    }
}
