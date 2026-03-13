using Domain.Contracts;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extensions;
public static class WebApplicationServices
{
    public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var objOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
        await objOfDataSeeding.SeedDataAsync();
        await objOfDataSeeding.SeedIdentityDataAsync();
        return app;
    }
    public static WebApplication UseExceptionHandlingMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        return app;
    }
    public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}