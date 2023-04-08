using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Extensions;
using Daisy.Infrastructure.Implementations.Interfaces;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Auth;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Daisy.Infrastructure.Implementations.Services
{
    internal sealed class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IClaimsService claimsService;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(IUnitOfWork UnitOfWork, IMapper Mapper, SignInManager<AppUser> SignInManager, UserManager<AppUser> UserManager, IConfiguration Configuration, IClaimsService ClaimsService, IJwtTokenService JwtTokenService, IHttpContextAccessor HttpContextAccessor)
        {
            unitOfWork= UnitOfWork;
            mapper= Mapper;
            signInManager= SignInManager;
            userManager= UserManager;
            configuration= Configuration;
            claimsService= ClaimsService;
            jwtTokenService= JwtTokenService;
            httpContextAccessor= HttpContextAccessor;

        }


        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await userManager.FindByNameAsync(loginRequest.UserName);
                if (user == null && !await userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                    return new LoginResponse { Successful = false, Message = "Invalid username and password combination (User does not exist." };
                }

                var userClaims = await claimsService.GetUserClaimsAsync(user);
                var userToken = jwtTokenService.GetJwtToken(userClaims);

                return user != null ? new LoginResponse { Successful = true, Message = "Success!", Token = new JwtSecurityTokenHandler().WriteToken(userToken), IsAuthenticated = true, UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName } : new LoginResponse { Successful = false, Message = "Invalid username and password combination", IsAuthenticated = false };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponse> LoginWithSignInManager(LoginRequest loginRequest)
        {
            try
            {
                var user = await userManager.FindByNameAsync(loginRequest.UserName);
                switch (user)
                {
                    case null:
                        return new LoginResponse { Successful = false, Message = "User does not exist." };
                    
                    default:
                        case var obj when user != null:
                            switch (await userManager.CheckPasswordAsync(user, loginRequest.Password))
                            {
                                case false:
                                    return new LoginResponse { Successful = false, Message = "Invalid username and password combination." };

                                case true:
                                    switch (new PasswordHasher<AppUser>().VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password))
                                    {
                                        case PasswordVerificationResult.Failed:
                                            return new LoginResponse { Successful = false, Message = "Password verification failed." };

                                        case PasswordVerificationResult.Success:
                                            break;

                                        case PasswordVerificationResult.SuccessRehashNeeded:
                                            var passHash = new PasswordHasher<AppUser>().HashPassword(user, loginRequest.Password);
                                            //save the hashed password to db
                                            break;

                                        default:
                                            break;
                                    }
                                
                                break;
                                
                            }
                        break;
                }

                var userClaims = await claimsService.GetUserClaimsAsync(user);
                var userToken = jwtTokenService.GetJwtToken(userClaims);
                if (!IsTokenValid(userToken))
                {
                    throw new SecurityTokenValidationException($"Error|Token is Invalid");
                }

                var request = new MapperConfiguration(cfg => cfg.CreateMap<LoginRequest, AppUser>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, LoginResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<LoginRequest, AppUser>(loginRequest);
                AppUser userToLogin = await unitOfWork.Auth.LoginWithSignInManager(destination);
                var result = responseMap.Map<AppUser, LoginResponse>(userToLogin);

                return result.Successful == true ? new LoginResponse { Successful = true, Message = "Login Successful!", Token = new JwtSecurityTokenHandler().WriteToken(userToken), UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, IsAuthenticated = true } : new LoginResponse { Successful = false, Message = "Invalid username/ password combination", IsAuthenticated = false };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LogoutResponse> LogoutWithSignInManager(LogoutRequest logoutRequest)
        {
            try
            {
                var userId = await userManager.FindByIdAsync(logoutRequest.Id.ToString());
                var userName = await userManager.FindByNameAsync(logoutRequest.UserName);
                if (userId == null && userName == null)
                {
                    return new LogoutResponse { Id = logoutRequest.Id, Successful = false, Message = "Logout unsucessful - Please confirm user details." };
                }

                if (userId != null && userName == null)
                {
                    return new LogoutResponse { Id = logoutRequest.Id, Successful = false, Message = $"Logout unsucessful - User with ID '{logoutRequest.Id}' does not seem to exist!" };
                }

                if (userId == null && userName != null)
                {
                    return new LogoutResponse { Id = logoutRequest.Id, Successful = false, Message = $"Logout unsucessful - User with Username '{logoutRequest.UserName}' does not seem to exist!" };
                }

                var request = new MapperConfiguration(cfg => cfg.CreateMap<LogoutRequest, AppUser>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, LogoutResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<LogoutRequest, AppUser>(logoutRequest);
                AppUser userToLogin = await unitOfWork.Auth.LogoutWithSignInManager(destination);
                var result = responseMap.Map<AppUser, LogoutResponse>(userToLogin);

                return result != null ? new LogoutResponse { Successful = true, Message = "Logout Successful", Id = logoutRequest.Id } : new LogoutResponse { Successful = false, Message = "Logout unsucessful. Please contact system administrator." };

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(forgotPasswordRequest.Email);
                if (user == null)
                {
                    return new ForgotPasswordResponse { Successful = false, Message = "NotFound|Sorry, we could not find a user with the specified email" };
                }

                var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);

                var request = new MapperConfiguration(cfg => cfg.CreateMap<ForgotPasswordRequest, AppUser>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.Equals(destMember))));
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, ForgotPasswordResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map(forgotPasswordRequest, user);
                destination.PasswordResetToken = passwordResetToken;
                

                try
                {
                    AppUser appUser = unitOfWork.AppUsers.Update(destination);
                    await unitOfWork.CompleteAsync();
                    var result = responseMap.Map<AppUser, ForgotPasswordResponse>(appUser);
                    return new ForgotPasswordResponse { Successful = true, Id = user.Id, Token = passwordResetToken };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                AuthExtensions.DecodePasswordResetToken(resetPasswordRequest.Token, out string decodedKey);

                resetPasswordRequest.Token = decodedKey;
                var user = userManager.Users.FirstOrDefault(user => user.PasswordResetToken == decodedKey);
                if (user == null)
                {
                    return new ResetPasswordResponse { Successful = false, Message = "NotFound|Sorry, we could not find a user with the specified email - Reset Key might be invalid" };
                }

                var request = new MapperConfiguration(cfg => cfg.CreateMap<ResetPasswordRequest, AppUser>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, ResetPasswordResponse>());
                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = request.CreateMapper();

                var destination = requestMap.Map<ResetPasswordRequest, AppUser>(resetPasswordRequest);
                destination.PasswordResetToken = resetPasswordRequest.Token;
                var result = await unitOfWork.Auth.ResetPassword(destination);

                return result.Successful == true ? new ResetPasswordResponse { Successful = true, Message = $"Your password has been reset. Please <a class = " + " nav-link active" + " aria-current= " + "page" + "@onclick=" + "@(() => Login())>click here to log in</a>" } : new ResetPasswordResponse { Successful = false, Message = $"{result.Message}", Errors = result.Errors};
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResetPasswordResponse> ResetUserPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var user = await userManager.FindByIdAsync(resetPasswordRequest.Id.ToString());
                if (user == null)
                {
                    return new ResetPasswordResponse { Successful = false, Message = "NotFound|Sorry, somehow we could not find this user" };
                }

                var result = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", await ValidatePasswordResetToken(resetPasswordRequest.Token));
                if (!result)
                {
                    return new ResetPasswordResponse { Successful = false, Message = $"Invalid password reset key" };
                }

                var passReset = await userManager.ResetPasswordAsync(user, user.PasswordResetToken, resetPasswordRequest.Password);
                List<string> errors = new();
                foreach (var error in passReset.Errors)
                {
                    errors.Add(error.Description);
                }

                return passReset.Succeeded ? new ResetPasswordResponse { Successful = true, Message = "Password reset successfully!" } : new ResetPasswordResponse { Successful = false, Message = "Password reset failed.", Errors = errors };

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> ValidatePasswordResetToken(string token) 
        {
            try
            {
                byte[] tokenBytes = WebEncoders.Base64UrlDecode(token);
                string decodedToken = Encoding.UTF8.GetString(tokenBytes);

                return decodedToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PassResetTokenValidationResponse> ValidateJwtPasswordResetToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var payload = tokenHandler.ReadJwtToken(token);
                var email = payload.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var expirationDateUnix = long.Parse(payload.Claims.FirstOrDefault(c => c.Type == "exp")?.Value);
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationDateUnix);
                var currentTime = DateTimeOffset.UtcNow;
                if (expirationDateTime <= currentTime)
                {
                    return new PassResetTokenValidationResponse { Successful = false, Message = $"Password reset key is expired. Please regenerate at Forgot Password" };
                }

                var user = await userManager.FindByEmailAsync(email);
                if (user.PasswordResetToken != token)
                {
                    return new PassResetTokenValidationResponse { Successful = false, Message = $"Invalid password reset key" };
                }

                return new PassResetTokenValidationResponse { Successful = true, Message = $"Valid" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsTokenValid(SecurityToken token)
        {
            try
            {
                if (token == null)
                {
                    throw new ArgumentException("Token is null");
                }

                AuthExtensions.SecurityKey(out string key);
                var clockSkew = Convert.ToDouble(configuration["Auth:Jwt:ClockSkew"]);
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(clockSkew),
                    ValidIssuer = configuration["Auth:Jwt:JwtIssuer"],
                    ValidAudience = configuration["Auth:Jwt:JwtAudience"],
                    IssuerSigningKey = token.SigningKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                };

                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(jwtSecurityTokenHandler.WriteToken(token), tokenValidationParameters, out var securityToken);

                    return true;
                }
                catch (SecurityTokenValidationException ex)
                {
                    throw new SecurityTokenValidationException($"Error|Token validation failed|{ex.Message}");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
