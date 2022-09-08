using HistoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly HistoryDbContext _context;

        public VisitorRepository(HistoryDbContext context) => _context = context;

        public void CreateVisitor(Visitor visitor)
        {
            if(visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            _context.Visitors.Add(visitor);
        }

        public IEnumerable<Visitor> GetAllVisitors() => _context.Visitors.AsNoTracking().ToList();

        public Visitor? GetVisitorById(int id) => _context.Visitors.AsNoTracking().FirstOrDefault(visitor => visitor.Id == id);
        
        public bool IsVisitorExists(int id) => _context.Visitors.Any(visitor => visitor.Id == id);

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
