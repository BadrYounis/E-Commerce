namespace Shared.Dtos.BasketModule;
public record BasketDto
{
    public string Id { get; init; } = string.Empty;
    public ICollection<BasketItemDto> BasketItems { get; init; } = [];
}