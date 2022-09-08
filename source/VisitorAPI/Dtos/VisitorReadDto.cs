namespace VisitorAPI.Dtos
{
    public class VisitorReadDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public DateTime Birthday { get; set; }

        public int Age { get; set; }
    }
}
