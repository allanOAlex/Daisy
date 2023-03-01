using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.User
{
    public class ResetPasswordRequest
    {
        public int Id { get; set; }
        public string? Password { get; set; }    
        public string? Token { get; set; }    
    }
}
