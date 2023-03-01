using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IRepositories;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Daisy.Infrastructure.Implementations.Repositories;
using Daisy.Infrastructure.Implementations.Services;
using Microsoft.AspNetCore.Identity;

namespace Daisy.Infrastructure.Implementations.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAppUserRepository AppUsers { get; private set; }
        public IEventRepository Events { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public ICarouselRepository Carousels { get; private set; }
        public IAuthRepository Auth { get; private set; }
        public IRoleRepository Roles { get; private set; }


        private readonly DBContext context;
        private readonly DapperContext daper;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;


        public UnitOfWork(DBContext Context, DapperContext Daper, UserManager<AppUser> UserManager, SignInManager<AppUser> SignInManager)
        {
            context = Context;
            daper = Daper;
            userManager = UserManager;
            signInManager = SignInManager;

            AppUsers = new AppUserRepository(this, context, daper, userManager);
            Events = new EventRepository(context);
            Appointments = new AppointmentRepository(context);
            Carousels = new CarouselRepository(context);
            Auth = new AuthRepository(context, signInManager, userManager);
        }


        public Task CompleteAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
