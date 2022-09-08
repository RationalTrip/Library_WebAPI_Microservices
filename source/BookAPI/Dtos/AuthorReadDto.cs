namespace BookAPI.Dtos
{
    public class AuthorReadDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;
    }
}
