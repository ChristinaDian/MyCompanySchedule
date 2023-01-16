using System.ComponentModel;

namespace MyCompanySchedule.ViewModels.Users
{
    public class FilterVM
    {
        [DisplayName("Username: ")]
        public string Username { get; set; }
        [DisplayName("First Name: ")]
        public string FirstName { get; set; }
        [DisplayName("Last Name: ")]
        public string LastName { get; set; }
    }
}
