using System.ComponentModel.DataAnnotations;

namespace VisitorAPI.Models
{
    public class Visitor
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string SecondName { get; set; } = string.Empty;

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public int Age
        {
            get
            {
                int yearDiff = DateTime.Now.Year - Birthday.Year;

                if (Birthday.AddYears(yearDiff) >= DateTime.Now.Date)
                    yearDiff++;

                return yearDiff;
            }
        }
    }
}
