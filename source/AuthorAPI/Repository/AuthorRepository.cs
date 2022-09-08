using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AuthorDbContext _context;

        public AuthorRepository(AuthorDbContext context) => _context = context;
        public void CreateAuthor(Author author)
        {
            if(author == null)
                throw new ArgumentNullException(nameof(author));

            _context.Authors.Add(author);
        }

        public IEnumerable<Author> GetAllAuthors() => _context.Authors.AsNoTracking().ToList();

        public Author? GetAuthorById(int id)
        {
            return _context.Authors.Where(author => author.Id == id)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
