namespace BorrowedAPI.Dtos
{
    public class BorrowedRecordReadDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int VisitorId { get; set; }

        public DateTime BorrowedDate { get; set; }

        public DateTime ReturnDeadline { get; set; }
    }
}
