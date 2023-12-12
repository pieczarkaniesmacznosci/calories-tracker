using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                _logger.LogError(ex.Message);
                string message;
                switch (ex)
                {
                    case ArgumentException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        message = ex.Message;
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = ex.Message;
                        break;
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        message = "Unauthorized";
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Something went wrong while processing request.";
                        break;
                }

                string result = JsonSerializer.Serialize(new { Message = message });
                await response.WriteAsync(result);
            }
        }
    }
}
