using BookAPI.Models;

namespace BookAPI.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetAuthorById(int id);
        void CreateAuthor(Author author);
        bool IsAuthorExists(int id);

        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
        void CreateBook(Book book);

        bool SaveChanges();
    }
}
