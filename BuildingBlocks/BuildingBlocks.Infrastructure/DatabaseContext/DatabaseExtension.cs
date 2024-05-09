using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.DatabaseContext;

public static class DatabaseExtension
{
    public static IServiceCollection RegisterDatabaseService(this IServiceCollection serviceCollection)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST", EnvironmentVariableTarget.Process);
        var dbName = Environment.GetEnvironmentVariable("DB_NAME", EnvironmentVariableTarget.Process);
        var dbUser = Environment.GetEnvironmentVariable("DB_USER", EnvironmentVariableTarget.Process);
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD", EnvironmentVariableTarget.Process);
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT", EnvironmentVariableTarget.Process);

        var connectionString = $"Data Source={dbHost};Database={dbName};persist security info=True;user id={dbUser};password={dbPassword};MultipleActiveResultSets=True;TrustServerCertificate=true;";

        if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword) || string.IsNullOrEmpty(dbPort))
            throw new ArgumentNullException(connectionString);
        serviceCollection.AddDbContextFactory<AutomobileDbContext>(options => options.UseSqlServer(connectionString));
        return serviceCollection;
    }
}
