namespace Shared.Dtos;
public record ProductResultDto(
    int Id,
    string Name,
    string Description,
    string PictureUrl,
    decimal Price,
    string BrandName,
    string TypeName
);