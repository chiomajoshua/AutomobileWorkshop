using BuildingBlocks.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace BuildingBlocks.Infrastructure.Middleware;

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
        app.UseExceptionHandler(
            appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = ApiStatusConstants.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
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