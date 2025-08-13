using Market.Application.Presistences.Repos.Contracts.Interfaces;
using Market.Application.Presistences.UnitofWork;
using Market.Infrastucture.Persistence;
using Market.Infrastucture.Repositories;
using Market.Infrastucture.Seeders;
using Market.Infrastucture.UnitofWorkPattren;
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

        // Add the Unit of Work and Lazy Resolver
        services.AddScoped<IUnitofWork, UnitofWork>();
        services.AddScoped(typeof(Lazy<>), typeof(LazyResolver<>));

        // 
        services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepositoy<>));
        services.AddScoped<IContactRepo, ContactRepo>();
        // Register other services, repositories, etc.
        // Example: services.AddScoped<IYourRepository, YourRepository>();
        services.AddScoped<ICountryKeySeeder, CountryKeySeeder>();
    }
}
