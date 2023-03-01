using Daisy.Application.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.Interfaces
{
    public interface IServiceManager
    {
        ICarouselService CarouselService { get; }
        IEventService EventService { get; }
        IAppointmentService AppointmentService { get; }
        IAppUserService AppUserService { get; }
        IAuthService AuthService { get; }
        IRoleService RoleService { get; }
        IEmailService EmailService { get; }
    }
}
