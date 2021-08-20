using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyExpensesAPI.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyExpensesAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsJsonAsync(new ErrorDetails
            {
                Message = Resources.Strings.InternalServerError,
                StatusCode = httpContext.Response.StatusCode
            });
        }
    }
}
