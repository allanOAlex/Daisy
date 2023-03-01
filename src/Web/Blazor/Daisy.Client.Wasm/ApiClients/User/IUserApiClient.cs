using Daisy.Shared.Requests.Event;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;

namespace Daisy.Client.Wasm.ApiClients.User
{
    public interface IUserApiClient
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        Task<List<GetAllUsersResponse>> GetAllUsers();
        Task<GetUserByIdResponse> GetById(GetUserByIdRequest request);
        Task<UpdateUserResponse> UpdateUser(int Id, UpdateUserRequest request);
        Task<DeleteUserResponse> DeleteUser(int Id, DeleteUserRequest request);
    }
}
