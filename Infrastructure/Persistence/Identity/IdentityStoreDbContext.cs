using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence.Identity;
public class IdentityStoreDbContext : IdentityDbContext
{
    public IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Address>().ToTable("Addresses");
    }
}