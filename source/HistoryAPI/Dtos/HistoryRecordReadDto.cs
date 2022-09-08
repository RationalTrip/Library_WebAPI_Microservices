namespace HistoryAPI.Dtos
{
    public class HistoryRecordReadDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int VisitorId { get; set; }

        public DateTime BorrowedDate { get; set; }

        public DateTime ReturnedDate { get; set; }

        public bool IsReturnedLate { get; set; }
    }
}
