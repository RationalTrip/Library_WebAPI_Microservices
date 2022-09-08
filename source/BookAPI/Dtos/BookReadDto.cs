namespace BookAPI.Dtos
{
    public class BookReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime PublicationDate { get; set; }

        public string? About { get; set; }

        public int AuthorId { get; set; }
    }
}
