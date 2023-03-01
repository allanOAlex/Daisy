using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Models;
using Daisy.Shared.Dtos;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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


        public AuthController(SignInManager<AppUser> SignInManager, IServiceManager ServiceManager, IConfiguration Configuration)
        {
            serviceManager = ServiceManager;
            signInManager = SignInManager;
            configuration = Configuration;
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
            var response = await serviceManager.AuthService.ForgotPassword(request);
            if (!response.Successful == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            } 

            var passwordForgotResponse = await serviceManager.EmailService.SendPasswordResetEmail(request.Email);
            return passwordForgotResponse.Successful == true ? StatusCode(StatusCodes.Status200OK, passwordForgotResponse) : StatusCode(StatusCodes.Status500InternalServerError, passwordForgotResponse);
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var passwordResetResponse = await serviceManager.AuthService.ResetPassword(request);

            return passwordResetResponse.Successful == true ? StatusCode(StatusCodes.Status200OK, passwordResetResponse) : StatusCode(StatusCodes.Status500InternalServerError, passwordResetResponse);
        }

    }
}
