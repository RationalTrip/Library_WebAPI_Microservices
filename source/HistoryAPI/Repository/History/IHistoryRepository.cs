using HistoryAPI.Models;

namespace HistoryAPI.Repository
{
    public interface IHistoryRepository
    {
        IEnumerable<HistoryRecord> GetAllHistory();
        IEnumerable<HistoryRecord> GetVisitorHistory(int visitorId);
        IEnumerable<HistoryRecord> GetBookHistory(int bookId);
        HistoryRecord? GetHistoryRecordById(int id);
        void CreateHistoryRecord(HistoryRecord historyRecord);

        bool IsBookExists(int id);
        bool IsVisitorExists(int id);

        bool SaveChanges();
    }
}
