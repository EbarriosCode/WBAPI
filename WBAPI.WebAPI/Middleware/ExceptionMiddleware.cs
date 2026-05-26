using FluentValidation;
using System.Net;
using System.Text.Json;

namespace WBAPI.WebAPI.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                // FluentValidation — errores del pipeline MediatR
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var errors = ex.Errors.Select(e => e.ErrorMessage);

                var response = new
                {
                    success = false,
                    message = "Validation errors.",
                    errors
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    message = "Internal server error."
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
