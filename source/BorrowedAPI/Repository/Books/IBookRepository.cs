using BorrowedAPI.Models;

namespace BorrowedAPI.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
        void CreateBook(Book book);
        bool IsBookExists(int id);

        bool SaveChanges();
    }
}
