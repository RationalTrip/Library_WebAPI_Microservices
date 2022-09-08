using BorrowedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowedAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BorrowedDbContext _context;

        public BookRepository(BorrowedDbContext context) => _context = context;

        public void CreateBook(Book book)
        {
            if(book == null)
                throw new ArgumentNullException(nameof(book));

            _context.Books.Add(book);
        }

        public IEnumerable<Book> GetAllBooks() => _context.Books.AsNoTracking().ToList();

        public Book? GetBookById(int id) => _context.Books.AsNoTracking().FirstOrDefault(book => book.Id == id);

        public bool IsBookExists(int id) => _context.Books.Any(book => book.Id == id);

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
