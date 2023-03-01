using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Appointments
{
    public class CreateAppointmentRequest
    {
        [Required(ErrorMessage = "Salutation is required")]
        public int Salutation { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Only 3 or more alphabetic characters allowed")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name cannot have less than 3 characters and more than 20 characters in length")]
        public string? FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Only 3 or more alphabetic characters allowed")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Middle Name cannot have less than 3 characters and more than 20 characters in length")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Only 3 or more alphabetic characters allowed")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name cannot have less than 3 characters and more than 20 characters in length")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Please provide a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Please type N/A if not applicable")]
        public string? PreferedLocation { get; set; }

        public string? Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        
        
    }
}
