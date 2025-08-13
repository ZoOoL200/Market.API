using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Market.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ApplicationServices (this IServiceCollection services)
    {
        // Register application services here
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        // Example: services.AddScoped<IMyService, MyService>();
        return services;
    }
}
