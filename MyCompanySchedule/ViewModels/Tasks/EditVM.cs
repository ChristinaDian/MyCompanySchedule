using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyCompanySchedule.ViewModels.Tasks
{
    public class EditVM
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "*This field is required!")]
        public string Title { get; set; }

        [DisplayName("Deadline")]
        [Required(ErrorMessage = "*Please enter a valid date!")]
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
    }
}
