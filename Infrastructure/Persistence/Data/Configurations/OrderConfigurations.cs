using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;
internal class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.ShippingAddress, sh => sh.WithOwner());
        builder.HasMany(o => o.OrderItems).WithOne();
        builder.Property(o => o.PaymentStatus).HasConversion(
            ps => ps.ToString(), ps => Enum.Parse<OrderPaymentStatus>(ps));
        builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
        builder.Property(o => o.SubTotal).HasColumnType("decimal(18, 4)");
    }
}