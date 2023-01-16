using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompanySchedule.Entities
{
    public class ToDoList : BaseEntity
    {
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; } = false;

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }   
    }
}
