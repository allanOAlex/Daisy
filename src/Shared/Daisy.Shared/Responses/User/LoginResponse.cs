using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.User
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool Successful { get; set; }
        public string? Message { get; set; }
        public List<Claim>? UserClaims { get; set; }

    }
}
