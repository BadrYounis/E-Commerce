namespace Shared.Dtos.OrderModule;
public record OrderRequest
{
    public string BasketId { get; init; } = string.Empty;
    public AddressDto ShippingAddress { get; init; }
    public int DeliveryMethodId { get; init; }
}