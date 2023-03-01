using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Daisy.Client.Wasm.AuthProviders
{
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsIdentity anonymous = new ClaimsIdentity();
        private ClaimsPrincipal identityUser = new ClaimsPrincipal();
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1000);

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }

        public async void LoginNotify()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Jon Doe"),
                new Claim(ClaimTypes.Role, "Super_User")
            };

            var identity = new ClaimsIdentity(claims, "testAuthType");
            anonymous = identity;
            identityUser = new ClaimsPrincipal(anonymous);
            
            await GetAuthenticationStateAsync();
        }

        public async void LogoutNotify()
        {
            anonymous = new ClaimsIdentity();
            identityUser = new ClaimsPrincipal(anonymous);
            await GetAuthenticationStateAsync();
        }
    }
}
