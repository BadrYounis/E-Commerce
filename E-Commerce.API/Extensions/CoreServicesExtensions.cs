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
        services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICacheService, CacheService>();

        services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>()
        );
        services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>()
        );
        services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>()
        );
        services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>()
        );
        services.AddScoped<Func<IPaymentService>>(provider =>
            () => provider.GetRequiredService<IPaymentService>()
        );
        services.AddScoped<Func<ICacheService>>(provider =>
            () => provider.GetRequiredService<ICacheService>()
        );

        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        return services;
    }
}