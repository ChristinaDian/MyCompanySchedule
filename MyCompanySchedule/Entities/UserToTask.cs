using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompanySchedule.Entities
{
    public class UserToTask : BaseEntity
    {
        public int UserId { get; set; }
        public int TodolistId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("TodolistId")]
        public virtual ToDoList ToDoList { get; set; }
    }
}
