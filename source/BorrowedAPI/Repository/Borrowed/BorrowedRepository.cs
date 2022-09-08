using BorrowedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowedAPI.Repository
{
    public class BorrowedRepository : IBorrowedRepository
    {
        private readonly BorrowedDbContext _context;

        public BorrowedRepository(BorrowedDbContext context) => _context = context;

        public void CreateBorrowedRecord(BorrowedRecord historyRecord)
        {
            if(historyRecord == null)
                throw new ArgumentNullException(nameof(historyRecord));

            _context.BorrowedRecords.Add(historyRecord);
        }

        public IEnumerable<BorrowedRecord> GetAllBorrowedRecords() => _context.BorrowedRecords.AsNoTracking().ToList();

        public IEnumerable<BorrowedRecord> GetBookBorrowedRecords(int bookId)
        {
            return _context.BorrowedRecords.Where(history => history.BookId == bookId)
                .AsNoTracking()
                .ToList();
        }

        public BorrowedRecord? GetBorrowedRecordById(int id)
        {
            return _context.BorrowedRecords.AsNoTracking().FirstOrDefault(history => history.Id == id);
        }

        public IEnumerable<BorrowedRecord> GetVisitorBorrowedRecords(int visitorId)
        {
            return _context.BorrowedRecords.Where(history => history.VisitorId == visitorId)
                .AsNoTracking()
                .ToList();
        }

        public bool IsBookExists(int id) => _context.Books.Any(book => book.Id == id);

        public bool IsVisitorExists(int id) => _context.Visitors.Any(visitor => visitor.Id == id);

        public void RemoveBorrowedRecord(BorrowedRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            _context.BorrowedRecords.Remove(record);
        }

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
