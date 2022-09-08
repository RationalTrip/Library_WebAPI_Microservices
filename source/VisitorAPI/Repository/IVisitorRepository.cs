using VisitorAPI.Models;

namespace VisitorAPI.Repository
{
    public interface IVisitorRepository
    {
        IEnumerable<Visitor> GetAllVisitors();
        Visitor? GetVisitorById(int id);
        void CreateVisitor(Visitor visitor);
        bool SaveChanges();
    }
}
