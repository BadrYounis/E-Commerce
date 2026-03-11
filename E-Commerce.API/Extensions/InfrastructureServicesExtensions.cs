using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using StackExchange.Redis;

namespace E_Commerce.API.Extensions;
public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IDataSeeding, DataSeeding>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        });
        services.AddScoped<IBasketRepository, BasketRepository>();
        return services;
    }
}