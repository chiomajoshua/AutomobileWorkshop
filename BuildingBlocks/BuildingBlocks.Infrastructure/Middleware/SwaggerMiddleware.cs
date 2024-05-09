using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace BuildingBlocks.Infrastructure.Middleware;

/// <summary>
/// 
/// </summary>
public static class SwaggerMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerService(this IServiceCollection services, string apiName, string apiVersion, string description, OpenApiContact contact)
    {
        _ = services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = apiName,
                Version = apiVersion,
                Description = description,
                Contact = contact,
                Extensions = new Dictionary<string, IOpenApiExtension>
            {
                {"x-logo", new OpenApiObject
                    {
                        {"url", new OpenApiString("#")},
                        { "altText", new OpenApiString("Box Commerce Logo")}
                    }
                }
            }
            });
            c.CustomSchemaIds(x => x.FullName);
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());           
            c.EnableAnnotations();
        });
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app)
    {
        app.UseSwagger(c => c.SerializeAsV2 = true);
        app.UseSwaggerUI();
        return app;
    }
}
