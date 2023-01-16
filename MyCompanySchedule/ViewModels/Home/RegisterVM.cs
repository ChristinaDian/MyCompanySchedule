using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyCompanySchedule.ViewModels.Home
{
    public class RegisterVM
    {
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Username { get; set; }

        [DisplayName("Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Password { get; set; }

        [DisplayName("First Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string FirstName { get; set; }

        [DisplayName("Last Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string LastName { get; set; }
        [DisplayName("Company Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string CompanyName { get; set; }
        public string? Role { get; set; }
    }
}
