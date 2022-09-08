using System.ComponentModel.DataAnnotations;

namespace VisitorAPI.Dtos
{
    public class VisitorCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string SecondName { get; set; } = string.Empty;

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
