using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using My.Application.Exceptions;
using My.Application.Exceptions.Contact;
using My.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteApi.Extensions
{
    /// <summary>
    /// ExceptionMiddlewareExtensions sınıfı, ASP.NET Core uygulamanızda hata işleme ve loglama için kullanılan bir ara yazılımdır. 
    /// Bu kod parçası, uygulamanızda meydana gelen hataları yakalamak ve bu hatalarla ilgili bilgileri kaydetmek amacıyla yapılandırılmıştır. 
    /// İşte bu kodun detaylı açıklaması:
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(apperror => 
            
            {
                apperror.Run(async context =>
                {
                   
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)//eger hata varsa bilgi, id vs yanlış girilmişşe falan burası devreye girerek  log yazar ve hatyı dönderir
                    {
                        context.Response.StatusCode = contextFeature.Error switch 
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError

                        };
                        logger.LogError($"Something went wrong : {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            statusCode = context.Response.StatusCode,
                            message =  contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
