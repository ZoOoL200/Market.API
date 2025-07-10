using Market.Infrastucture.Persistence;
using Market.Infrastucture.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastucture.Extension;

public static class ServiceCollectionsExtension
{
    public static void AddMarketInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        // Register your DbContext with the dependency injection container
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("localConnection")));

        // Register other services, repositories, etc.
        // Example: services.AddScoped<IYourRepository, YourRepository>();
        services.AddScoped<ICountryKeySeeder, CountryKeySeeder>();
    }
}
