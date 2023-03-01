using Daisy.Shared.Requests.Event;
using Daisy.Shared.Requests.User;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IAppUserService
    {
        Task<CreateUserResponse> Create(CreateUserRequest request);
        Task<List<GetAllUsersResponse>> GetAllUsers();
        Task<GetUserByIdResponse> GetById(int Id);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
        Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request);


    }
}
