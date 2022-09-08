namespace LibraryTransit.Contract.Dtos.Authors
{
    public class AuthorPublishDto : IAuthorPublishDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;
    }
}
