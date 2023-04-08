using Azure;
using Blazored.LocalStorage;
using Daisy.Client.Wasm.AuthProviders;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Daisy.Client.Wasm.ApiClients.Auth
{
    public class AuthApiClient : IAuthApiClient 
    {
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient client;
        private readonly IConfiguration config;
        private string? responseMessage;

        public AuthApiClient(AuthenticationStateProvider AuthStateProvider, ILocalStorageService LocalStorage, HttpClient Client, IConfiguration Config)
        {
            authStateProvider= AuthStateProvider;
            localStorage= LocalStorage;
            client= Client; 
            config= Config;

            client.Timeout = TimeSpan.FromMinutes(10);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Auth:Login"], request);
            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponse() { Message = $"Error validating user | Invalid username and password combination |  {response.ReasonPhrase}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var authenticatedUser = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

            await localStorage.SetItemAsync("authToken", authenticatedUser.Token);
            //((ApiAuthenticationStateProvider)authStateProvider).MarkUserAsAuthenticated(request.UserName);
            ((ApiAuthenticationStateProvider)authStateProvider).DoMarkUserAsAuthenticated(authenticatedUser);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticatedUser.Token);

            return authenticatedUser;

        }

        public async Task<LoginResponse> LoginWithSignInManager(LoginRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Auth:LoginWithSignInManager"], request);
            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponse() { Successful = false, Message = $"Error logging in | {response.ReasonPhrase}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var authenticatedUser = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

            await localStorage.SetItemAsync("authToken", authenticatedUser.Token);
            ((ApiAuthenticationStateProvider)authStateProvider).MarkUserAsAuthenticated(authenticatedUser);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticatedUser.Token);
            
            return authenticatedUser;

        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)authStateProvider).MarkUserAsLoggedOut();
            client.DefaultRequestHeaders.Authorization = null;

        }

        public async Task<LogoutResponse> LogoutWithSignInManager(LogoutRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Auth:LogoutWithSignInManager"], request);
            if (!response.IsSuccessStatusCode)
            {
                return new LogoutResponse() { Successful = false, Message = $"Error logging out | Please contact system admin | {response.ReasonPhrase}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var loggedOutUser = JsonConvert.DeserializeObject<LogoutResponse>(apiResponse);

            await localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)authStateProvider).MarkUserAsLoggedOut();
            client.DefaultRequestHeaders.Authorization = null;
            return loggedOutUser;

        }

        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Auth:ForgotPassword"], request);
            if (!response.IsSuccessStatusCode)
            {
                return new ForgotPasswordResponse() { Successful = false, Message = $"Something went wrong | Please contact system admin " };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var passwordForgotResponse = JsonConvert.DeserializeObject<ForgotPasswordResponse>(apiResponse);
            return passwordForgotResponse;

        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Auth:ResetPassword"], request);
            if (!response.IsSuccessStatusCode)
            {
                var failedApiResponse = await response.Content.ReadAsStringAsync();
                var failedPasswordResetResponse = JsonConvert.DeserializeObject<ResetPasswordResponse>(failedApiResponse);
                string errors = string.Join(", ", failedPasswordResetResponse.Errors);
                return new ResetPasswordResponse() { Successful = false, Message = $"Password reset failed. Please contact system admin | Errors: - {errors} " };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var passwordResetResponse = JsonConvert.DeserializeObject<ResetPasswordResponse>(apiResponse);
            return passwordResetResponse;

        }

        public async Task<ResetPasswordResponse> ResetUserPassword(string userId, string token)
        {
            var response = await client.GetFromJsonAsync<ResetPasswordResponse>($"{config["Api:Routes:Auth:ResetPassword"]}{userId}/{token}");
            if (!response.Successful)
            {
                string errors = string.Join(", ", response.Errors);
                return new ResetPasswordResponse() { Successful = false, Message = $"Password reset failed. Please contact system admin | Errors: - {errors} " };
            }

            return response;

        }


    }
}
