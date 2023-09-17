using System.Net;
using System.Text.Json;
using Api.Exceptions;
using Api.Models;

namespace Api.Middlewares
{
    /// <summary>
    /// This middleware is intended to handle all Exceptions thrown by application.
    /// It wraps custom business logic exceptions into correct response.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
       private const string DefaultErrorMessage = "A server error occurred.";

       private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
       {
           PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
       };

        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        

        public ErrorHandlingMiddleware(RequestDelegate next, IHostEnvironment environment, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                _logger.LogWarning(unauthorizedEx, "Unauthorized access");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            catch (BusinessLogicException ex)
            {
                await HandleCustomException(context, ex, HttpStatusCode.BadRequest);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleCustomException(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleUnknownException(context, ex);
            }
        }

        private async Task HandleCustomException(HttpContext context, PaylocityCustomException ex, HttpStatusCode statusCode)
        {
            var apiError = new BaseApiResponse
            {
                Success = false,
                Error = ex.Code ?? string.Empty,
                Message = ex.Message
            };
            var response = context.Response;
            response.ContentType = "application/json";
            
            // get the response code and message
            response.StatusCode = (int) statusCode;
            await response.WriteAsync(JsonSerializer.Serialize(apiError, JsonSerializerOptions));
        }

        private async Task HandleUnknownException(HttpContext context, Exception ex)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
            }

            var apiError = new BaseApiResponse
            {
                Success = false,
                Message = _environment.IsDevelopment() ? ex.Message : DefaultErrorMessage
            };
            var response = context.Response;
            response.ContentType = "application/json";
            
            // get the response code and message
            response.StatusCode = StatusCodes.Status500InternalServerError;
            await response.WriteAsync(JsonSerializer.Serialize(apiError, JsonSerializerOptions));

            _logger.LogError(ex, "An unhandled exception occured while processing request {identifier}", context.TraceIdentifier);
        }
    }
}