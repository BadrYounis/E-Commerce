namespace Shared.Dtos.OrderModule;
public record OrderResult
{
    public Guid Id { get; init; }
    public string UserEmail { get; init; } = string.Empty;
    public AddressDto ShippingAddress { get; init; }
    public ICollection<OrderItemDto> OrderItems { get; init; } = [];
    public string PaymentStatus { get; init; } = string.Empty;
    public string DeliveryMethod { get; init; }
    public int? DeliveryMethodId { get; init; }
    public decimal SubTotal { get; init; }
    public decimal Total { get; init; }
    public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
    public string PaymentIntentId { get; init; } = string.Empty;
}