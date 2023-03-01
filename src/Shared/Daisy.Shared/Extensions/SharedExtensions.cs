using Daisy.Shared.Requests.Appointments;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Extensions
{
    public static class SharedExtensions
    {
        public static string valid = "Valid";
        public static string ToString(this DateTime? dt, string format) => dt == null ? "n/a" : ((DateTime)dt).ToString(format);

        public static string CreateAppointmentValidate(CreateAppointmentRequest model)
        {
            try
            {
                

                return valid;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
