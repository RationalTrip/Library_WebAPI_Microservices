using HistoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HistoryDbContext _context;

        public HistoryRepository(HistoryDbContext context) => _context = context;

        public void CreateHistoryRecord(HistoryRecord historyRecord)
        {
            if(historyRecord == null)
                throw new ArgumentNullException(nameof(historyRecord));

            _context.History.Add(historyRecord);
        }

        public IEnumerable<HistoryRecord> GetAllHistory() => _context.History.AsNoTracking().ToList();

        public IEnumerable<HistoryRecord> GetBookHistory(int bookId)
        {
            return _context.History.Where(history => history.BookId == bookId)
                .AsNoTracking()
                .ToList();
        }

        public HistoryRecord? GetHistoryRecordById(int id)
        {
            return _context.History.AsNoTracking().FirstOrDefault(history => history.Id == id);
        }

        public IEnumerable<HistoryRecord> GetVisitorHistory(int visitorId)
        {
            return _context.History.Where(history => history.VisitorId == visitorId)
                .AsNoTracking()
                .ToList();
        }

        public bool IsBookExists(int id) => _context.Books.Any(book => book.Id == id);

        public bool IsVisitorExists(int id) => _context.Visitors.Any(visitor => visitor.Id == id);

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
