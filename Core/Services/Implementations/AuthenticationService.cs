using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations;
public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
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
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.DisplayName),
            new (ClaimTypes.Email, user.Email!)
        };
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var signInCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4ce62a6c5e53b56620cc09c0bde8be2b515c83359379a3d4ea0dfb8b83c4e519")),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7162",
            audience: "Angular Project",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: signInCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}