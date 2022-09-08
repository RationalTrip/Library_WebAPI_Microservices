using HistoryAPI.Models;

namespace HistoryAPI.Repository
{
    public interface IVisitorRepository
    {
        IEnumerable<Visitor> GetAllVisitors();
        Visitor? GetVisitorById(int id);
        void CreateVisitor(Visitor visitor);
        bool IsVisitorExists(int id);

        bool SaveChanges();
    }
}
