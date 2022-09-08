using System.ComponentModel.DataAnnotations;

namespace BorrowedAPI.Models
{
    public class Visitor
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string SecondName { get; set; } = string.Empty;

        public List<BorrowedRecord> BorrowedHistory { get; set; } = new List<BorrowedRecord>();
    }
}
