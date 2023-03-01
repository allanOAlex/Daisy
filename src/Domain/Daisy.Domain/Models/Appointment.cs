using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Domain.Models
{
    public class Appointment
    {
        public Appointment()
        {

        }

        [Key]
        public int Id { get; set; }
        public int Salutation { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string? PreferedLocation { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }

        [DefaultValue(false)]
        public bool IsCancelled { get; set; } = false;
        public DateTime CancelledOn { get; set; }
        public int CancelledBy { get; set; }
        public bool IsComplete { get; set; } = false;
        public DateTime CompletedOn { get; set; }
        public int CompletedBy { get; set; }
        public string CompletedRemarks { get; set; }

    }
}
