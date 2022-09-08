using BorrowedAPI.Models;

namespace BorrowedAPI.Repository
{
    public interface IBorrowedRepository
    {
        IEnumerable<BorrowedRecord> GetAllBorrowedRecords();
        IEnumerable<BorrowedRecord> GetVisitorBorrowedRecords(int visitorId);
        IEnumerable<BorrowedRecord> GetBookBorrowedRecords(int bookId);
        BorrowedRecord? GetBorrowedRecordById(int id);
        void RemoveBorrowedRecord(BorrowedRecord record);
        void CreateBorrowedRecord(BorrowedRecord historyRecord);

        bool IsBookExists(int id);
        bool IsVisitorExists(int id);

        bool SaveChanges();
    }
}
