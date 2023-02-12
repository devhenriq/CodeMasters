using CodeMasters.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace CodeMasters.Domain.Handlers
{
    public static class DefaultExceptionHandler
    {
        public static void AddExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionFeature != null)
                    {
                        var httpStatusCode = exceptionFeature.Error switch
                        {
                            InvalidInputException => StatusCodes.Status400BadRequest,
                            EntityNotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.Error("Error message: {Error}", exceptionFeature.Error.Message);
                        context.Response.StatusCode = httpStatusCode;
                        context.Response.ContentType = Text.Plain;
                        await context.Response.WriteAsync(exceptionFeature.Error.Message);
                    }
                });
            });
        }
    }
}
