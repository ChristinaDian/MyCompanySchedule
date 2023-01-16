using MyCompanySchedule.Entities;
using MyCompanySchedule.ViewModels.Shared;

namespace MyCompanySchedule.ViewModels.Users
{
    public class IndexVM
    {
        public List<User> Items { get; set; }
        public FilterVM Filter { get; set; }
        public PagerVM Pager { get; set; }
    }
}
