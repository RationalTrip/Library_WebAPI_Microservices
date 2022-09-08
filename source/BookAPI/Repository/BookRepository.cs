using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context) => _context = context;

        public void CreateAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            _context.Authors.Add(author);
        }

        public void CreateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _context.Books.Add(book);
        }

        public IEnumerable<Author> GetAllAuthors() => _context.Authors.AsNoTracking().ToList();

        public IEnumerable<Book> GetAllBooks() => _context.Books.AsNoTracking().ToList();

        public Author? GetAuthorById(int id)
        {
            return _context.Authors.Where(author => author.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.Where(book => book.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public bool IsAuthorExists(int id) => _context.Authors.Any(author => author.Id == id);

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
