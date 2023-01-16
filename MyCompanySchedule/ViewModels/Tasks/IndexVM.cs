using MyCompanySchedule.Entities;
using MyCompanySchedule.ViewModels.Shared;

namespace MyCompanySchedule.ViewModels.Tasks
{
    public class IndexVM
    {
        public List<ToDoList> Items { get; set;}
        public FilterVM Filter { get; set; }
        public PagerVM Pager { get; set; }
    }
}
