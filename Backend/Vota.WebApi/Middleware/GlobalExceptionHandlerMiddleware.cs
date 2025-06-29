using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Vota.WebApi.Common;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vota.WebApi.Middleware
{
    /// <summary>
    /// Global exception exceptio handler middleware
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");

            var response = new ResponseViewModel();

            switch (exception)
            {
                case BusinessLogicException businessEx:
                    context.Response.StatusCode = (int)businessEx.StatusCode;
                    response.Code = businessEx.StatusCode;
                    response.Message = businessEx.Message;
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Code = HttpStatusCode.Unauthorized;
                    response.Message = "Request denied!";
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Code = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong!";
#if DEBUG
                    response.Details = new { exception.Message, exception.StackTrace };
#endif
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}

