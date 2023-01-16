using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompanySchedule.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CompanyId { get; set; }
        public int? WorkingHoursPerMonth { get; set; }
        public int? Department { get; set; }
        public string? Role { get; set; }
       
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
    }
}
