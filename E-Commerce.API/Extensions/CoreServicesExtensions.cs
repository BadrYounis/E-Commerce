using Services;
using Services.Abstraction.Contracts;
using Services.Implementations;
using Shared.Common;

namespace E_Commerce.API.Extensions;
public static class CoreServicesExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
        services.AddScoped<IServiceManager, ServiceManager>();
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        return services;
    }
}