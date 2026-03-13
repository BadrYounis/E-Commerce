using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistence.Data;
public class DataSeeding(
    StoreDbContext _dbContext,
    RoleManager<IdentityRole> _roleManager,
    UserManager<User> _userManager) : IDataSeeding
{
    public async Task SeedDataAsync()
    {
        try
        {
            //check if there are any pending migrations => apply to database
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
            if (!_dbContext.ProductBrands.Any())
            {
                var productBrandsData = File.OpenRead("..\\Infrastructure\\Persistence\\Data\\DataSeed\\brands.json");
                //JSON => C# Object [List<ProductBrands>]
                var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                if (productBrands is not null && productBrands.Any())
                    await _dbContext.ProductBrands.AddRangeAsync(productBrands);
            }
            if (!_dbContext.ProductTypes.Any())
            {
                var productTypesData = File.OpenRead("..\\Infrastructure\\Persistence\\Data\\DataSeed\\types.json");
                var productTypes =await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                if (productTypes is not null && productTypes.Any())
                    await _dbContext.ProductTypes.AddRangeAsync(productTypes);
            }
            if (!_dbContext.Products.Any())
            {
                var productsData = File.OpenRead("..\\Infrastructure\\Persistence\\Data\\DataSeed\\products.json");
                var products =await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                if (products is not null && products.Any())
                    await _dbContext.Products.AddRangeAsync(products);
            }
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Handle Exception
        }
    }
    public async Task SeedIdentityDataAsync()
    {
        try
        {
            //1) Seed roles [Admin, SuperAdmin]
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            //2) Seed users [AdminUser, SuperAdminUser]
            if (!_userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01005068014"
                };
                var superAdminUser = new User()
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01017151157"
                };
                await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                await _userManager.CreateAsync(superAdminUser, "Pa$$w0rd");
                //3) Assign roles to users
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}