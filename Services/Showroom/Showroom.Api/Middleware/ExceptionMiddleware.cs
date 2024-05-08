using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace ShowroomService.Api.Middleware;

/// <summary>
/// 
/// </summary>
public static class ExceptionMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                if (contextFeature != null)
                {
                    Log.Error($"ExceptionFailure: {JsonConvert.SerializeObject(contextFeature.Error)}.");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        IsSuccessful = false,                        
                        Message = "Oops! An error occurred while processing your request. If this persists after three(3) trials, kindly contact your administrator.",
                        StatusCode = 500
                    }));
                }
            });
        });
    }
}