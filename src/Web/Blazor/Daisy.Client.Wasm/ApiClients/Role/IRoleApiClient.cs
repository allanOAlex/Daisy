using Daisy.Shared.Responses.Role;
using Daisy.Shared.Responses.User;

namespace Daisy.Client.Wasm.ApiClients.Role
{
    public interface IRoleApiClient
    {
        Task<List<GetAllRolesResponse>> GetAllRoles();
    }
}
