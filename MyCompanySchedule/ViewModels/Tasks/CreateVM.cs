using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyCompanySchedule.ViewModels.Tasks
{
    public class CreateVM
    {
        public int OwnerId { get; set; }

        [DisplayName("Title: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Title { get; set; }
       
        [DisplayName("Deadline: ")]
        [Required(ErrorMessage = "*Please enter a valid date!")]
        public DateTime Deadline { get; set; }   
    }
}
