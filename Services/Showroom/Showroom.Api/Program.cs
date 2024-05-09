using BuildingBlocks.Infrastructure.DatabaseContext;
using BuildingBlocks.Infrastructure.InternetClient.Contracts;
using BuildingBlocks.Infrastructure.InternetClient.Implementation;
using BuildingBlocks.Infrastructure.Middleware;
using BuildingBlocks.Infrastructure.PipelineBehaviours;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using BuildingBlocks.Infrastructure.RabbitMq.Implementation;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Serilog;
using ShowroomService.Core.BackgroundServices;
using ShowroomService.Core.Handlers.Vehicles;
using ShowroomService.Core.Services.Components.Contracts;
using ShowroomService.Core.Services.Components.Implementation;
using ShowroomService.Core.Services.Vehicles.Contracts;
using ShowroomService.Core.Services.Vehicles.Implementation;
using System.Globalization;

Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.CreateBootstrapLogger();

Log.Information("Starting up");

var myAllowedOrigins = "_myAllowedOrigins";

try
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        ApplicationName = typeof(Program).Assembly.FullName,
        ContentRootPath = Directory.GetCurrentDirectory(),
    });

    builder.Host.UseSerilog((ctx, lc) => lc
      .WriteTo.Console()
      .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.RegisterDatabaseService();
    builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

    builder.Services.AddHttpClient();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowedOrigins,
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
    });
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddLogging();
    builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
    builder.Services.AddHealthChecks();

    builder.Services.RegisterPipelineBehaviours();
    builder.Services.AddTransient<IHttpClientService, HttpClientService>();
    builder.Services.AddTransient<IRabbitMqProducer, RabbitMqProducer>();
    builder.Services.AddTransient<IComponentService, ComponentService>();
    builder.Services.AddTransient<IVehicleService, VehicleService>();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetVehicleByIdHandler>());
    builder.Services.AddHostedService<ProcessOrderHostedService>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerService("Showroom Service API", "v1", "Automobile Workshop Production Service API", new OpenApiContact
    {
        Name = "AutoMobile"
    });

    var app = builder.Build();

    app.ConfigureExceptionHandler();
    app.UseSerilogRequestLogging();
    var supportedCultures = new[] { new CultureInfo("en-GB"), new CultureInfo("en-US") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en-GB"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures,
    });
    app.UseSwaggerService();
    app.UseNWebSecurity();
    app.UseCors(myAllowedOrigins);
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseResponseCaching();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("Showroom Service is shutting down");
}