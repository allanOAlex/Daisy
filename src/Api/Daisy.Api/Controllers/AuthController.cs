using Daisy.Application.Abstractions.Interfaces;
using Daisy.Domain.Models;
using Daisy.Shared.Dtos;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Policy;
using System.Text;

namespace Daisy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private static AppUserDto LoggedOutUser = new AppUserDto { IsAuthenticated = false };
        private readonly SignInManager<AppUser> signInManager;
        private readonly IServiceManager serviceManager;
        private readonly IConfiguration configuration;
        private readonly LinkGenerator linkGenerator;


        public AuthController(SignInManager<AppUser> SignInManager, IServiceManager ServiceManager, IConfiguration Configuration, LinkGenerator LinkGenerator)
        {
            serviceManager = ServiceManager;
            signInManager = SignInManager;
            configuration = Configuration;
            linkGenerator = LinkGenerator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest model)
        {
            var loggedInUser = await serviceManager.AuthService.Login(model);

            return loggedInUser.Successful == true ? StatusCode(StatusCodes.Status200OK, loggedInUser) : StatusCode(StatusCodes.Status401Unauthorized, new LoginResponse { Successful = loggedInUser.Successful, Message = $"{loggedInUser.Message}." });

        }

        [AllowAnonymous]
        [HttpPost("loginwithsigninmanager")]
        public async Task<ActionResult<LoginResponse>> LoginWithSignInManager(LoginRequest model)
        {
            var loggedInUser = await serviceManager.AuthService.LoginWithSignInManager(model);
            return loggedInUser.Successful == true ? StatusCode(StatusCodes.Status200OK, loggedInUser) : StatusCode(StatusCodes.Status401Unauthorized, loggedInUser);

        }

        [AllowAnonymous]
        [HttpPost("logoutwithsigninmanager")]
        public async Task<ActionResult<LogoutResponse>> LogoutWithSignInManager(LogoutRequest model)
        {
            var loggedOutUser = await serviceManager.AuthService.LogoutWithSignInManager(model);
            return loggedOutUser.Successful == true ? StatusCode(StatusCodes.Status200OK, loggedOutUser) : StatusCode(StatusCodes.Status500InternalServerError, loggedOutUser);

        }

        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                var response = await serviceManager.AuthService.ForgotPassword(request);
                if (!response.Successful == true)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                var passwordForgotResponse = await serviceManager.EmailService.SendPasswordResetEmail(request.Email, response.Token, response.ResetUrl);

                return passwordForgotResponse.Successful == true ? StatusCode(StatusCodes.Status200OK, passwordForgotResponse) : StatusCode(StatusCodes.Status500InternalServerError, passwordForgotResponse);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var passwordResetResponse = await serviceManager.AuthService.ResetUserPassword(request);

            return passwordResetResponse.Successful == true ? StatusCode(StatusCodes.Status200OK, passwordResetResponse) : StatusCode(StatusCodes.Status500InternalServerError, passwordResetResponse);
        }

        [AllowAnonymous]
        [HttpPost("resetpassword/{userId}/{token}")]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(string userId, string token)
        {
            var request = new ResetPasswordRequest
            {
                Id = int.Parse(userId),
                Token = token,
                Password = "paS5@Auth",
                PasswordConfirm = "paS5@Auth"
            };

            //return Redirect($"passwordreset/{userId}/{token}");
            var passwordResetResponse = await serviceManager.AuthService.ResetUserPassword(request);
            return passwordResetResponse.Successful == true ? StatusCode(StatusCodes.Status200OK, passwordResetResponse) : StatusCode(StatusCodes.Status500InternalServerError, passwordResetResponse);
        }


    }
}
