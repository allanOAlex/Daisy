using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Models;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Services
{
    internal sealed class RoleService : IRoleService
    {
        private readonly IUnitOfWork? unitOfWork;
        private readonly IMapper? mapper;
        private readonly UserManager<AppUser>? userManager;

        public RoleService()
        {

        }

        public async Task<AddToRoleResponse> AddToRole(AddToRoleRequest request)
        {
            try
            {
                AppUser userId = await userManager.FindByIdAsync(request.UserId.ToString());
                AppUser userName = await userManager.FindByNameAsync(request.UserName);

                if (userId == null && userName != null)
                {
                    await userManager.AddToRoleAsync(userName, request.RoleName);
                    return new AddToRoleResponse() { UserId = request.UserId, RoleId = request.RoleId, RoleName = request.RoleName, Successful = true, Message = $"{userName.FirstName} has been added to {request.RoleName}!" };

                }
                else if (userId != null)
                {
                    await userManager.AddToRoleAsync(userId, request.RoleName);
                    return new AddToRoleResponse() { UserId = request.UserId, RoleId = request.RoleId, RoleName = request.RoleName, Successful = true, Message = $"{userId.FirstName} has been added to {request.RoleName}!" };
                }

                return new AddToRoleResponse() { Successful = false, Message = $"Failed | {request.FirstName} was not added to the role." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
