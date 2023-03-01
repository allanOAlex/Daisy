using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Daisy.Shared.Responses.Appointments
{
    public class GetAllAppointmentsResponse
    {
        public int Id { get; set; }
        public int Salutation { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Phone")]
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        [Display(Name = "Location")]
        public string? PreferedLocation { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancelledOn { get; set; }
        public int CancelledBy { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CompletedOn { get; set; }
        public int CompletedBy { get; set; }
        public string? CompletedRemarks { get; set; }
    }
}
