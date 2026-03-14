using E_Commerce.API.Extensions;

namespace E_Commerce.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            #region DI Container
            var builder = WebApplication.CreateBuilder(args);

            //WebApi Services
            builder.Services.AddWebApiServices();

            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //Core Services
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion

            #region Pipelines (Middlewares)
            var app = builder.Build();

            await app.SeedDatabaseAsync();

            app.UseExceptionHandlingMiddlewares();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}