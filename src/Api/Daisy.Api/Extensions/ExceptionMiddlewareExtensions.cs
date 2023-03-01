using Daisy.Api.Middleware;
using Daisy.Shared.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Daisy.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
