using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Daisy.Shared.Responses.Appointments
{
    public class CreateAppointmentResponse
    {
        public int Id { get; set; }
        public string? Salutation { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        public string? Email { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Appointment Time")]
        public DateTime Time { get; set; }

        [Display(Name = "Prefered Location")]
        public string? PreferedLocation { get; set; }

        public string? Remarks { get; set; }

        public bool Successful { get; set; }
        public string? Message { get; set; }

    }
}
