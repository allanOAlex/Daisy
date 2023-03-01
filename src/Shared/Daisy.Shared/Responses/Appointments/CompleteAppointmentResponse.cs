using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.Appointments
{
    public class CompleteAppointmentResponse
    {
        public bool Successful { get; set; }
        public string? Message { get; set; }
    }
}
