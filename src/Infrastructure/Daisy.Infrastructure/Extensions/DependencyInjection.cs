using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IRepositories;
using Daisy.Application.Abstractions.IServices;
using Daisy.Infrastructure.Implementations.Interfaces;
using Daisy.Infrastructure.Implementations.Repositories;
using Daisy.Infrastructure.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Daisy.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICarouselRepository, CarouselRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ICarouselService, CarouselService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddTransient<IClaimsService, ClaimsService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IEmailService, EmailService>();

            return services;


        }
    }
}
