using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;

namespace Daisy.Client.Wasm.ApiClients.Auth
{
    public interface IAuthApiClient
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<LoginResponse> LoginWithSignInManager(LoginRequest request);
        Task Logout();
        Task<LogoutResponse> LogoutWithSignInManager(LogoutRequest request);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        Task<ResetPasswordResponse> ResetUserPassword(string userId, string token);
    }
}
