using Daisy.Client.Wasm.ApiClients.User;
using Daisy.Shared.Responses.Role;
using Microsoft.Extensions.Logging;

namespace Daisy.Client.Wasm.ApiClients.Role
{
    public class RoleApiClient : IRoleApiClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;

        public RoleApiClient(HttpClient Client, IConfiguration Config)
        {
            client = Client;
            config = Config;
        }

        public Task<List<GetAllRolesResponse>> GetAllRoles()
        {
            throw new NotImplementedException();
        }
    }
}
