namespace Shared.Dtos.IdentityModule;
public record UserResultDto(
    string DisplayName,
    string Token,
    string Email
);