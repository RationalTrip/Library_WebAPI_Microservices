using System.ComponentModel.DataAnnotations;

namespace HistoryAPI.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public List<HistoryRecord> BorrowedHistory { get; set; } = new List<HistoryRecord>();
    }
}
