using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Extensions;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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

        public AuthService(IUnitOfWork UnitOfWork, IMapper Mapper, SignInManager<AppUser> SignInManager, UserManager<AppUser> UserManager, IConfiguration Configuration, IClaimsService ClaimsService, IJwtTokenService JwtTokenService)
        {
            unitOfWork= UnitOfWork;
            mapper= Mapper;
            signInManager= SignInManager;
            userManager= UserManager;
            configuration= Configuration;
            claimsService= ClaimsService;
            jwtTokenService= JwtTokenService;
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

                return new ForgotPasswordResponse { Successful = true, Message = "Password Reset Successful!", Id = user.Id };
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
                var user = await userManager.FindByIdAsync(resetPasswordRequest.Id.ToString());
                if (user == null)
                {
                    return new ResetPasswordResponse { Successful = false, Message = "NotFound|Sorry, we could not find a user with the specified email" };
                }

                var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);

                var request = new MapperConfiguration(cfg => cfg.CreateMap<ResetPasswordRequest, AppUser>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, ForgotPasswordResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<ResetPasswordRequest, AppUser>(resetPasswordRequest);
                AppUser userToLogin = await unitOfWork.Auth.ResetPassword(destination, passwordResetToken);
                var result = responseMap.Map<AppUser, ResetPasswordResponse>(userToLogin);

                return result.Successful == true ? new ResetPasswordResponse { Successful = true, Message = $"Your password has been reset. Please <a class = " + " nav-link active" + " aria-current= " + "page" + "@onclick=" + "@(() => Login())>click here to log in</a>" } : new ResetPasswordResponse { Successful = false, Message = "There was a problem updating your password. Please contact system administrator." };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
