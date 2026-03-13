using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts;
public interface IAuthenticationService
{
    Task<UserResultDto> LoginAsync(LoginDto loginDto);
    Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
}
