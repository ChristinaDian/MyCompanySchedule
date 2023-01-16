using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyCompanySchedule.ViewModels.Users
{
    public class CreateVM
    {
        [DisplayName("Username: ")]
        [Required(ErrorMessage="*This field is Required!")]
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
        public int? CompanyId { get; set; }
        public int? WorkingHoursPerMonth { get; set; }
        public int? Department { get; set; }
        public string? Role { get; set; }
    }
}
