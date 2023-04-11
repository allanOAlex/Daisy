using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
//using Newtonsoft.Json;
using System.Text.Json;
using Daisy.Shared.Responses.User;
using Daisy.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Daisy.Infrastructure.Extensions;

namespace Daisy.Client.Wasm.AuthProviders
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationState anonymous;

        public ApiAuthenticationStateProvider(HttpClient HttpClient, ILocalStorageService LocalStorage)
        {
            httpClient= HttpClient;
            localStorage= LocalStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Anonymous"),
                    new Claim(ClaimTypes.Role, "User")
                }, "jwt")));
            }

            AuthExtensions.savedToken = savedToken;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        }

        public void MarkUserAsAuthenticated(string username)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsAuthenticated(LoginResponse user)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] 
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim("Username", user.UserName ),
                new Claim("Firstname", user.FirstName),
                new Claim("Lastname", user.LastName),
                new Claim(ClaimTypes.Name, string.Concat(user.FirstName, " ",  user.LastName))
            }, "apiauth"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void DoMarkUserAsAuthenticated(LoginResponse user)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Name, string.Concat(user.FirstName, " ",  user.LastName)),
            }, "apiauth"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            try
            {
                var claims = new List<Claim>();
                var userClaims = new Dictionary<string, List<Claim>>();
                var header = jwt.Split('.')[0];
                var payload = jwt.Split('.')[1];
                var signature = jwt.Split('.')[2];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
                var keyValuePairs1 = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                keyValuePairs.TryGetValue(ClaimTypes.Role, out object? roles);
                keyValuePairs1.TryGetValue("claims", out object? jwtClaims);

                if (roles != null)
                {
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parsedRoles = System.Text.Json.JsonSerializer.Deserialize<string[]>(roles.ToString());

                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                if (jwtClaims != null)
                {
                    if (jwtClaims.ToString().Trim().StartsWith("["))
                    {
                        var jsonArray = JArray.Parse(jwtClaims.ToString());
                        foreach (var data in jsonArray)
                        {
                            var type = (string)data["Type"];
                            var value = (string)data["Value"];

                            if (type == ClaimTypes.Role)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, value));
                            }

                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, jwtClaims.ToString()));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

                return claims;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }


    }
}
