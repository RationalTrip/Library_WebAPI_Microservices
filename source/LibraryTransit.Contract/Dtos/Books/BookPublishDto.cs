namespace LibraryTransit.Contract.Dtos.Books
{
    public class BookPublishDto : IBookPublishDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
