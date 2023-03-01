using Daisy.Application.Abstractions.IRepositories;
using Daisy.Application.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.Interfaces
{
    public interface IUnitOfWork
    {
        ICarouselRepository Carousels { get; }
        IAppointmentRepository Appointments { get; }
        IEventRepository Events { get; }
        IAppUserRepository AppUsers { get; }
        IAuthRepository Auth { get; }
        IRoleRepository Roles { get; }

        Task CompleteAsync();
    }
}
