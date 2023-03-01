using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GetJwtToken(List<Claim> userClaims);
        SecurityToken GetToken(List<Claim> userClaims);
        string JwtToken(List<Claim> userClaims);
    }
}
