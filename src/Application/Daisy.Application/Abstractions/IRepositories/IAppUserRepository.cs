using Daisy.Domain.Models;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IRepositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        Task<AppUser> CreateWithUserManager(AppUser entity);
        Task<AppUser> DoUpdate(AppUser entity);
        AppUser DoDapperUpdate(AppUser entity);
        Task<AppUser> DapperUpdate(AppUser entity);
        AppUser DoDapperDelete(AppUser entity);
        Task<AppUser> DapperDelete(AppUser entity);
    }
}
