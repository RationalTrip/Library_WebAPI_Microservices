using Microsoft.EntityFrameworkCore;
using VisitorAPI.Models;

namespace VisitorAPI.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorDbContext _context;

        public VisitorRepository(VisitorDbContext context) => _context = context;

        public void CreateVisitor(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
        }

        public IEnumerable<Visitor> GetAllVisitors() => _context.Visitors.AsNoTracking().ToList();

        public Visitor? GetVisitorById(int id)
        {
            return _context.Visitors.Where(visitor => visitor.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
