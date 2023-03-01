using BlazorStrap.InternalComponents;
using Daisy.Infrastructure.Extensions;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;

namespace Daisy.Client.Wasm.ApiClients.User
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;
        private readonly ILogger<UserApiClient> logger;
        private string? responseMessage;

        public UserApiClient(HttpClient Client, IConfiguration Config, ILogger<UserApiClient> Logger)
        {
            client = Client;
            config = Config;
            logger = Logger;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            logger.LogInformation($"{nameof(UserApiClient)}|(Create User)Calling the API.");
            var response = await client.PostAsJsonAsync(config["Api:Routes:AppUser:Create"], request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(UserApiClient)}|(Create User)API response not sucessful.", response);
                return new CreateUserResponse() { Successful = false, Message = $"Error Creating User | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<CreateUserResponse>(apiResponse);

            logger.LogInformation($"{nameof(UserApiClient)}|(Create User)User created successfully.");
            return createdUser;

        }

        public async Task<DeleteUserResponse> DeleteUser(int Id, DeleteUserRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:AppUser:DeleteUser"]}{Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(UserApiClient)}|(Delete User)API response not sucessful.", response);
                return new DeleteUserResponse() { Successful = false, Message = $"Error deleting user | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var deletedUser = JsonConvert.DeserializeObject<DeleteUserResponse>(apiResponse);

            return deletedUser;

        }

        public async Task<List<GetAllUsersResponse>> GetAllUsers()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, config["Api:Routes:AppUser:GetAllUsers"]);

            var response = await client.GetFromJsonAsync<List<GetAllUsersResponse>>(config["Api:Routes:AppUser:GetAllUsers"]);
            if (response.Count <= 0)
            {
                logger.LogError($"{nameof(UserApiClient)}|(GetAllUsers)|No Records Found");
                return new List<GetAllUsersResponse>();
            }

            return response;
        }

        public async Task<GetUserByIdResponse> GetById(GetUserByIdRequest request)
        {
            var response = await client.GetFromJsonAsync<GetUserByIdResponse>($"{config["Api:Routes:AppUser:GetById"]}{request.Id}");

            if (!response.Successful)
            {
                logger.LogError($"{nameof(UserApiClient)}|(GetById)|Error while fetching user with Id {request.Id}");
                return new GetUserByIdResponse() { Successful = false, Message = $"Error fetching user" };
            }

            logger.LogError($"{nameof(UserApiClient)}|(GetById)|User with Id {request.Id} returned");
            return new GetUserByIdResponse() { Successful = true, Message = $"User with Id {request.Id} found" };
        }

        public async Task<UpdateUserResponse> UpdateUser(int Id, UpdateUserRequest request)
        {
            try
            {
                var response = await client.PutAsJsonAsync($"{config["Api:Routes:AppUser:UpdateUser"]}{Id}", request);
                responseMessage = response.ReasonPhrase;
                if (!response.IsSuccessStatusCode)
                {
                    return new UpdateUserResponse() { Successful = false, Message = $"Error updating user | {responseMessage}" };
                }

                var apiResponse = await response.Content.ReadAsStringAsync();
                var updatedUser = JsonConvert.DeserializeObject<UpdateUserResponse>(apiResponse);

                return updatedUser;
            }
            catch (Exception)
            {
                throw;
            }
            
        }


    }
}
