using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError => 
            {
                appError.Run(async context =>
                { 
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null) 
                    {
                        context.Response.StatusCode = contextFeature.Error switch // Modern switch case kullanımı
                        {
                            NotFoundExeption => StatusCodes.Status404NotFound, //case1
                            _ => StatusCodes.Status500InternalServerError      //case2
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                        
                    }
                });               
            });
        }
    }
}
