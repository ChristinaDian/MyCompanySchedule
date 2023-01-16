using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompanySchedule.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Subscription { get; set; } = "free";

    }
}
