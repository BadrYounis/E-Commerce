using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers;
public class AuthenticationController(IServiceManager _serviceManager) : ApiController
{
    //Register
    [HttpPost("Register")]
    public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
        => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));

    //Login
    [HttpPost("Login")]
    public async Task<ActionResult<UserResultDto>> LoginAsync([FromBody] LoginDto loginDto)
        => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));

    //GetCurrentUser
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        => Ok(await _serviceManager.AuthenticationService.GetCurrentUserAsync(User.FindFirstValue(ClaimTypes.Email)!));

    //CheckEmailExists
    [HttpGet("EmailExists")]
    public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
        => Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));

    //GetUserAddressAsync
    [Authorize]
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        => Ok(await _serviceManager.AuthenticationService.GetUserAddressAsync(User.FindFirstValue(ClaimTypes.Email)!));

    //UpdateUserAddressAsync
    [Authorize]
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync([FromBody] AddressDto addressDto)
        => Ok(await _serviceManager.AuthenticationService.UpdateUserAddressAsync(User.FindFirstValue(ClaimTypes.Email)!, addressDto));
}