using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Exceptions.ModelExceptions;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Daisy.Shared.Extensions;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Xml;
using static Dapper.SqlMapper;

namespace Daisy.Infrastructure.Implementations.Services
{
    internal sealed class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        //private readonly ILogger<AppUserService> logger;

        public AppUserService(IUnitOfWork UnitOfWork, IMapper Mapper, UserManager<AppUser> UserManager)
        {
            unitOfWork = UnitOfWork;    
            mapper = Mapper;
            userManager = UserManager;  
        }

        public async Task<CreateUserResponse> Create(CreateUserRequest createUserRequest)
        {
            try
            {
                createUserRequest.CreatedOn = DateTime.Now;
                createUserRequest.CreatedBy = 1;

                PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();

                var usernameExists = await userManager.FindByNameAsync(createUserRequest.UserName);
                var emailExists = await userManager.FindByEmailAsync(createUserRequest.Email);

                if (usernameExists != null || emailExists != null) { return new CreateUserResponse { Successful = false, Message = "Error|User already exists!" }; }

                var request = new MapperConfiguration(cfg => cfg.CreateMap<CreateUserRequest, AppUser>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, CreateUserResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CreateUserRequest, AppUser>(createUserRequest);
                destination.PasswordHash = ph.HashPassword(destination, destination.Password);
                destination.Password = destination.PasswordHash;
                destination.IsActive = true;
                AppUser userToCreate = await unitOfWork.AppUsers.CreateWithUserManager(destination);
                var result = responseMap.Map<AppUser, CreateUserResponse>(userToCreate);

                await unitOfWork.CompleteAsync();
                result.Successful = true;
                await userManager.AddToRoleAsync(userToCreate, "User");

                return result.Successful == true ? new CreateUserResponse { Successful = true, Message = "User created successfully!" } : new CreateUserResponse { Successful = false, Message = "Error|Creating user failed" };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllUsersResponse>> GetAllUsers()
        {
            try
            {
                List<GetAllUsersResponse> usersList = new();
                var users = unitOfWork.AppUsers.FindAll();
                if (users.Any())
                {
                    foreach (var item in users)
                    {
                        var listItem = new GetAllUsersResponse
                        {
                            Id = item.Id,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            Phone = item.PhoneNumber,
                        };

                        usersList.Add(listItem);
                    }

                    return usersList;
                }

                return usersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetUserByIdResponse> GetById(int Id)
        {
            try
            {
                var userToFind = unitOfWork.AppUsers.FindByCondition(e => e.Id == Id);
                if (userToFind.Any())
                {
                    var response = from e in userToFind
                    select new GetUserByIdResponse
                    {
                        Successful = true,
                        Message = $"User with Id {Id} found"
                    };

                    return response.FirstOrDefault();
                }

                return new GetUserByIdResponse() { Successful = false, Message = $"User with Id {Id} not found." };
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            try
            {
                updateUserRequest.UpdatedOn = DateTime.Now;
                updateUserRequest.UpdatedBy = updateUserRequest.UpdatedBy;

                var entity = unitOfWork.AppUsers.FindByCondition(e => e.Id == updateUserRequest.Id).AsNoTracking().FirstOrDefault();

                var request = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserRequest, AppUser>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember) => srcMember != null && !srcMember.Equals(destMember))));
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UpdateUserResponse>());
                
                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map(updateUserRequest, entity);
                AppUser userToUpdate = unitOfWork.AppUsers.Update(destination);
                var result = responseMap.Map<AppUser, UpdateUserResponse>(userToUpdate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new UpdateUserResponse() { Successful = true, Message = "User details updated successfully!" } : new UpdateUserResponse() { Successful = false, Message = "Error updating user" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var databaseValues = entry.GetDatabaseValues();
                    var clientValues = entry.CurrentValues;

                    if (databaseValues == null)
                    {
                        // The entity has been deleted from the database
                        // Handle this situation as appropriate
                    }
                    else
                    {
                        // The entity has been modified in the database
                        var databaseEntity = databaseValues.ToObject();
                        var clientEntity = clientValues.ToObject();

                        // Update the entity properties with the database values
                        foreach (var property in clientValues.Properties)
                        {
                            var databaseValue = databaseValues[property];
                            var clientValue = clientValues[property];

                            if (databaseValue != null && !databaseValue.Equals(clientValue))
                            {
                                clientValues[property] = databaseValue;
                            }
                        }

                        // Retry the update operation
                        await unitOfWork.CompleteAsync();
                    }
                    throw ex;
                }
            }
            catch (Exception)
            {
                throw;
            }
            

        }

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            try
            {
                deleteUserRequest.IsDeleted = true;
                deleteUserRequest.DeletedOn = DateTime.Now;
                deleteUserRequest.DeletedBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<DeleteUserRequest, AppUser>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, DeleteUserResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<DeleteUserRequest, AppUser>(deleteUserRequest);
                AppUser eventToCreate = unitOfWork.AppUsers.Update(destination);
                var result = responseMap.Map<AppUser, DeleteUserResponse>(eventToCreate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new DeleteUserResponse() { Successful = true, Message = "User deleted successfully!" } : new DeleteUserResponse() { Successful = false, Message = "Error deleting user" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
