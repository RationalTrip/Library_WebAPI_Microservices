using HistoryAPI.Models;

namespace HistoryAPI.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
        void CreateBook(Book book);
        public bool IsBookExists(int id);

        bool SaveChanges();
    }
}
