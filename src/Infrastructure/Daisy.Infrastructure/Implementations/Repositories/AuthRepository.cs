using Daisy.Application.Abstractions.IRepositories;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Repositories
{
    internal sealed class AuthRepository : IBaseRepository<AppUser>, IAuthRepository
    {
        private readonly DBContext context;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public AuthRepository(DBContext Context, SignInManager<AppUser> SignInManager, UserManager<AppUser> UserManager)
        {
            context= Context;
            signInManager= SignInManager;
            userManager= UserManager;
        }

        public AppUser Create(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public AppUser Delete(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AppUser> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<AppUser> FindByCondition(Expression<Func<AppUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> LogoutWithSignInManager(AppUser entity)
        {
            try
            {
                await signInManager.SignOutAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error | {ex.InnerException}");
            }
            
        }

        public async Task<ResetPasswordResponse> ResetPassword(AppUser entity)
        {
            try
            {
                var result = await userManager.ResetPasswordAsync(entity, entity.PasswordResetToken, entity.Password);
                if (result.Succeeded)
                {
                    return new ResetPasswordResponse { Successful = true, Message = "Password reset successfully!"};
                }
                else
                {
                    List<string> errors = new();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }

                    return new ResetPasswordResponse { Successful = false, Message = "Password reset failed.", Errors = errors};
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AppUser Update(AppUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
