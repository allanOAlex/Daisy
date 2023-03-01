using Azure.Core;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Extensions;
using Daisy.Shared.Dtos;
using Daisy.Shared.Enums;
using Daisy.Shared.Helpers;
using Daisy.Shared.Helpers.Authorization;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Daisy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenCORSPolicy")]

    public class AppUserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IServiceManager serviceManager;

        public AppUserController(UserManager<AppUser> UserManager, IServiceManager ServiceManager)
        {
            userManager= UserManager;
            serviceManager = ServiceManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest model)
        {
            var createdUser = await serviceManager.AppUserService.Create(model);
            return createdUser.Successful == true ? StatusCode(StatusCodes.Status201Created, new CreateUserResponse { Successful = true, Message = "User created successfully!" }) : StatusCode(StatusCodes.Status500InternalServerError, new CreateUserResponse { Successful = createdUser.Successful, Message = $"{createdUser.Message}" });

        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpGet("fetchall")]
        public async Task<ActionResult<List<GetAllUsersResponse>>> FetchAll()
        {
            var users = await serviceManager.AppUserService.GetAllUsers();

            if (users != null)
                return Ok(users);

            return users;

        }

        [HttpGet("getbyId/{Id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetById(int Id)
        {
            var userToFind = await serviceManager.AppUserService.GetById(Id);

            if (userToFind == null)
                return new GetUserByIdResponse() { Successful = false, Message = $"User with Id : {Id} not found" };

            return Ok(userToFind);

        }

        [HttpPut("update/{Id}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser(int Id, UpdateUserRequest request)
        {
            if (Id != request.Id)
                return new UpdateUserResponse() { Successful = false, Message = $"User ID mismatch" };

            var userToUpdate = await serviceManager.AppUserService.GetById(Id);

            if (userToUpdate == null)
                return new UpdateUserResponse() { Successful = false, Message = $"User with Id : {Id} not found" };

            var updatedUser = await serviceManager.AppUserService.UpdateUser(request);
            return Ok(updatedUser);

        }

        [HttpPut("delete/{Id}")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser(int Id, DeleteUserRequest request)
        {
            if (Id != request.Id)
                return new DeleteUserResponse() { Successful = false, Message = $"User ID mismatch" };

            var userToDelete = await serviceManager.AppUserService.GetById(Id);

            if (userToDelete == null)
                return new DeleteUserResponse() { Successful = false, Message = $"User with Id : {Id} not found" };

            var deletedUser = await serviceManager.AppUserService.DeleteUser(request);
            return Ok(deletedUser);
        }



    }
}
