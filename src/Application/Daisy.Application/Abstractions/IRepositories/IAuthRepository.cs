using Daisy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IRepositories
{
    public interface IAuthRepository : IBaseRepository<AppUser>
    {
        Task<AppUser> LoginWithSignInManager(AppUser entity);
        Task<AppUser> LogoutWithSignInManager(AppUser entity);
        Task<AppUser> ResetPassword(AppUser entity, string token);
    }
}
