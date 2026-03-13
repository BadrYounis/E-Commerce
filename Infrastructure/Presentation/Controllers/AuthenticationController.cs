using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.IdentityModule;

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
}