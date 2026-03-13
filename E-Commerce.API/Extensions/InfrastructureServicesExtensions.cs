using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
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
        services.AddDbContext<IdentityStoreDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });
        services.AddScoped<IDataSeeding, DataSeeding>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        });
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<IdentityStoreDbContext>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        return services;
    }
}