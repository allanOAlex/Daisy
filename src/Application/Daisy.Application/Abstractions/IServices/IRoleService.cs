using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IRoleService
    {
        Task<AddToRoleResponse> AddToRole(AddToRoleRequest request);
    }
}
