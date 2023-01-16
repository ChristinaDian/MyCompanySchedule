using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace MyCompanySchedule.ViewModels.Tasks
{
    public class FilterVM
    {
        [DisplayName("Title: ")]
        public string Title { get; set; }
        [DisplayName("Done?")]
        public bool? IsDone { get; set; } = null;
        [DisplayName("Owner:")]
        public bool OwnerTasks { get; set; }

    }
}
