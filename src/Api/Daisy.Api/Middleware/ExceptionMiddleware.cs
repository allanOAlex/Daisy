using Daisy.Shared.Dtos;
using Daisy.Shared.Exceptions.Global;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
   

        public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task InvokeIt(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);

                string errorId = Guid.NewGuid().ToString();
                var errorResult = new ErrorDetailsDto
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId,
                    SupportMessage = $"Provide the Error Id: {errorId} to the support team for further analysis."
                };

                errorResult.Messages.Add(exception.Message);

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                switch (exception)
                {
                    case CustomException e:
                        errorResult.StatusCode = (int)e.statusCode;
                        if (e.errorMessages is not null)
                        {
                            errorResult.Messages = e.errorMessages;
                        }
                        switch (errorResult.StatusCode)
                        {
                            case (int)HttpStatusCode.BadRequest:
                                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case (int)HttpStatusCode.Unauthorized:
                                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                break;
                            case (int)HttpStatusCode.Forbidden:
                                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                break;
                            case (int)HttpStatusCode.NotFound:
                                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;

                            default:
                                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }
                        break;

                    case KeyNotFoundException:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        if (exception.Message is not null)
                        {
                            errorResult.Messages.Add(exception.Message);
                        }
                        break;
                }

                var response = httpContext.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));

                }
                else
                {
                    logger.LogWarning("Can't write error response. Response has already started.");
                }

                throw new CustomException($"{errorResult}|{exception.Message}");
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context != null)
                {
                    await next(context);
                }
                
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task ProcessExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetailsDto()
            {
                StatusCode = context.Response.StatusCode,
                Message = $"(Custom Middleware)Error|{ex.Message}"
            }.ToString());
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                HttpStatusCode status = HttpStatusCode.OK;
                var stackTrace = string.Empty;
                string? message = string.Empty;
                string errorId = Guid.NewGuid().ToString();

                var errorResult = new ErrorDetailsDto
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId,
                    SupportMessage = $"Provide the Error Id: {errorId} to the support team for further analysis."
                };

                var exceptionType = exception.GetType();
                if (exceptionType.Name == nameof(BadRequestException))
                {
                    message = exception.Message;
                    status = HttpStatusCode.BadRequest;
                    stackTrace = exception.StackTrace;
                    logger.LogError(exceptionType.Name, exception);
                }
                else if (exceptionType.Name == nameof(ForbiddenException))
                {
                    status = HttpStatusCode.Forbidden;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(KeyNotFoundException))
                {
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(NotFoundException))
                {
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(NotImplementedException))
                {
                    status = HttpStatusCode.NotImplemented;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(UnauthorizedAccessException))
                {
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(DbUpdateConcurrencyException))
                {
                    status = HttpStatusCode.Conflict;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(NullReferenceException))
                {
                    status = HttpStatusCode.Forbidden;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else if (exceptionType.Name == nameof(InvalidOperationException))
                {
                    status = HttpStatusCode.Forbidden;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }
                else
                {
                    status = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                }

                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)status;
                    errorResult.StatusCode = (int)status;
                    errorResult.Message = message;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResult));

                }
                else
                {
                    logger.LogWarning("Can't write error response. Response has already started.");
                }

                //response.StatusCode = status;
                //response.Content = new StringContent($"(Custom Middleware)Error|{message}");

                //await context.Response.WriteAsync(response.ToString());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                throw;
            }
            

        }
    }
}
