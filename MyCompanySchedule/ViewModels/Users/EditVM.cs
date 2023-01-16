using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyCompanySchedule.ViewModels.Users
{
    public class EditVM
    {
        public int Id { get; set; }
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
        public int? CompanyId { get; set; }
        [DisplayName("Working hours per month: ")]
        public int? WorkingHoursPerMonth { get; set; }
        [DisplayName("Department: ")]
        public int? Department { get; set; }
        [DisplayName("Role: ")]
        public string? Role { get; set; }
    }
}
