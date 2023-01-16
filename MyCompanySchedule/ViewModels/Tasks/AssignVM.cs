using MyCompanySchedule.Entities;

namespace MyCompanySchedule.ViewModels.Tasks
{
    public class AssignVM
    {
        public int ToDoListId { get; set; }
        public List<int> UserIds { get; set; }
        public ToDoList ToDoList { get; set; }
        public List<UserToTask> Assignments { get; set; }
        public List<User> Users { get; set; }
    }
}
