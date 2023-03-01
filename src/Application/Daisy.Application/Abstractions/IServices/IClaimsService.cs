using Daisy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IClaimsService
    {
        Task<List<Claim>> GetUserClaimsAsync(AppUser appUser);
    }
}
