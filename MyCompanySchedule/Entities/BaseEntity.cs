using System.ComponentModel.DataAnnotations;

namespace MyCompanySchedule.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
