using System.ComponentModel.DataAnnotations;

namespace HistoryAPI.Models
{
    public class Visitor
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string SecondName { get; set; } = string.Empty;

        public List<HistoryRecord> BorrowedHistory { get; set; } = new List<HistoryRecord>();
    }
}
