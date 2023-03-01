using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<LoginResponse> LoginWithSignInManager(LoginRequest loginRequest);
        Task<LogoutResponse> LogoutWithSignInManager(LogoutRequest logoutRequest);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        bool IsTokenValid(SecurityToken token);
    }
}
