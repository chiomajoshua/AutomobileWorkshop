using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Showroom.Api.Middleware;

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
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        _ = services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Loan Credit Factory API",
                Version = "1.0",
                Description = "Loan Credit Product Factory",
                Contact = new OpenApiContact
                {
                    Name = "Application Support",
                    Email = "info@bazaratech.com"
                },
                Extensions = new Dictionary<string, IOpenApiExtension>
            {
                {"x-logo", new OpenApiObject
                    {
                        {"url", new OpenApiString("#")},
                        { "altText", new OpenApiString("Bazara Logo")}
                    }
                }
            }
            });
            c.CustomSchemaIds(x => x.FullName);
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
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
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(-1);
            c.SwaggerEndpoint("../swagger/v1/swagger.json", "Automobile Workshop Inventory Service");
        });

        app.UseReDoc(options =>
        {
            options.DocumentTitle = "Automobile Workshop Inventory Service";
            options.SpecUrl = "../swagger/v1/swagger.json";
        });
        return app;
    }
}
