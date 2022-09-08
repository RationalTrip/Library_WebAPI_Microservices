namespace AuthorAPI.Dtos
{
    public class AuthorReadDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public DateTime? DeathDate { get; set; }

        public string? About { get; set; }
    }
}
