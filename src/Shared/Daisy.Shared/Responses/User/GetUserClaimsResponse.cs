using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Responses.User
{
    public class GetUserClaimsResponse
    {
        public virtual List<Claim>? Claims { get; set; } 
    }
}
