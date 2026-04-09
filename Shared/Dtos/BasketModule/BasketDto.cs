namespace Shared.Dtos.BasketModule;
public record BasketDto
{
    public string Id { get; init; } = string.Empty;
    public ICollection<BasketItemDto> Items { get; init; } = [];
    public string? PaymentIntentId { get; init; }
    public string? ClientSecret { get; init; }
    public decimal? ShippingPrice { get; init; }
    public int DeliveryMethodId { get; init; }
}