using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using CrossCutting.Helpers;


namespace Api.CrossCutting.Configuration
{
    public static class ProblamDetailConfiguration
    {
        public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var problemDetails = new ProblemDetails
                        {
                            Instance = context.Request.HttpContext.Request.Path
                        };
                        if (exception is BadHttpRequestException badHttpRequestException)
                        {
                            problemDetails.Title = "The request is invalid";
                            problemDetails.Status = StatusCodes.Status400BadRequest;
                            problemDetails.Detail = badHttpRequestException.Message;
                        }
                        else
                        {
                            problemDetails.Title = exception.Message;
                            problemDetails.Status = StatusCodes.Status500InternalServerError;
                            problemDetails.Detail = GetExceptionStackTrace(exception);
                        }
                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";
                        var json = JsonConvert.SerializeObject(problemDetails, SerializerSettings.JsonSerializerSettings);
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }

        private static string GetExceptionStackTrace(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            var currentException = exception;

            while (currentException != null)
            {
                stringBuilder.AppendLine($"Type: {currentException.GetType().FullName}");
                stringBuilder.AppendLine($"Message: {currentException.Message}");
                stringBuilder.AppendLine("StackTrace:");
                stringBuilder.AppendLine(currentException.StackTrace);
                stringBuilder.AppendLine();

                currentException = currentException.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}