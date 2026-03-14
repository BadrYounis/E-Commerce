using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Common;
using Shared.Dtos.IdentityModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations;
public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> _options) : IAuthenticationService
{
    public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            throw new UnauthorizedException();

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
            throw new UnauthorizedException();

        return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email!);
    }
    public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Username,
            PhoneNumber = registerDto.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description).ToList();
            throw new ValidationException(errors);
        }
        return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
    }
    public async Task<string> CreateTokenAsync(User user)
    {
        var jwtOptions = _options.Value;
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.DisplayName),
            new (ClaimTypes.Email, user.Email!)
        };
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var signInCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays),
            signingCredentials: signInCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}