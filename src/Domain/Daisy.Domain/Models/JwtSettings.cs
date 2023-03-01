using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Domain.Models
{
    public class JwtSettings
    {
        public JwtSettings()
        {

        }

        public string? JwtSecutityKey { get; set; }
        public string? JwtIssuer { get; set; }
        public string? JwtAudience { get; set; }
        public int JwtExpiryInDays { get; set; }
    }
}
