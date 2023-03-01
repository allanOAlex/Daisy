using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.Appointments
{
    public class UpdateAppointmentRequest
    {
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
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
    }
}
