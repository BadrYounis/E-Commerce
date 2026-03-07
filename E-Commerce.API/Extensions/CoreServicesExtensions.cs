using Services;
using Services.Abstraction.Contracts;
using Services.Implementations;

namespace E_Commerce.API.Extensions;
public static class CoreServicesExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
        services.AddScoped<IServiceManager, ServiceManager>();
        return services;
    }
}