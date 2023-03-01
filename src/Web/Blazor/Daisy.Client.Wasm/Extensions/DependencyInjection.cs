using Daisy.Client.Wasm.ApiClients.Appointments;
using Daisy.Client.Wasm.ApiClients.Auth;
using Daisy.Client.Wasm.ApiClients.Carousel;
using Daisy.Client.Wasm.ApiClients.Event;
using Daisy.Client.Wasm.ApiClients.Role;
using Daisy.Client.Wasm.ApiClients.User;
using Daisy.Client.Wasm.AuthProviders;
using Daisy.Client.Wasm.Handlers.AuthHandlers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Daisy.Client.Wasm.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBlazorWasm(this IServiceCollection services)
        {
            services.AddScoped<ICarouselApiClient, CarouselApiClient>();
            services.AddScoped<IAppointmentApiClient, AppointmentApiClient>();
            services.AddScoped<IEventApiClient, EventApiClient>();
            services.AddScoped<IUserApiClient, UserApiClient>();
            services.AddScoped<IAuthApiClient, AuthApiClient>();
            services.AddScoped<IRoleApiClient, RoleApiClient>();

            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            //services.AddScoped<ApiAuthenticationStateProvider>();
            //services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ApiAuthenticationStateProvider>());

            services.AddTransient<ApiAuthorizationHandler>();

            return services;


        }
    }
}
