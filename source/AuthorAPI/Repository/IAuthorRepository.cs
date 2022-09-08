using AuthorAPI.Models;

namespace AuthorAPI.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetAuthorById(int id);
        void CreateAuthor(Author author);
        bool SaveChanges();
    }
}
