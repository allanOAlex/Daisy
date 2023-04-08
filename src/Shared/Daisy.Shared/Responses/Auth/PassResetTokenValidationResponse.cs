using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.Auth
{
    public class PassResetTokenValidationResponse
    {
        public bool Successful { get; set; }
        public string? Message { get; set; }
    }
}
