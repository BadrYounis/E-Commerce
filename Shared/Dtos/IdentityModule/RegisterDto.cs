using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule;
public record RegisterDto
{
    [EmailAddress]
    public string Email { get; init ; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; init; }
    public string Username { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
}