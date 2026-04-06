using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts;
public interface IAuthenticationService
{
    Task<UserResultDto> LoginAsync(LoginDto loginDto);
    Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    Task<UserResultDto> GetCurrentUserAsync(string userEmail);
    Task<bool> CheckEmailExistAsync(string userEmail);
    Task<AddressDto> GetUserAddressAsync(string userEmail);
    Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto);
}