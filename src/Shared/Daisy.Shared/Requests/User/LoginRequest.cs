using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Requests.User
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required"), MinLength(3, ErrorMessage = "Username must have a minimum of 3 characters")]
        public string? UserName { get; set; }

        public string? Password { get; set; }

        [DefaultValue(false)]
        public bool? RememberMe { get; set; } = false;
    }
}
