using Core;
using Microsoft.AspNetCore.Mvc;
using ProjectAuth.Application.DTOs;
using ProjectAuth.Application.Interfaces;
using ProjectPasswordManager.Application.DTOs;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthUserService _authUserService;

    public AuthController(IAuthUserService authUserService)
    {
        _authUserService = authUserService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthUserResponse>>> Register(AuthUserRegister authUserRegister, CancellationToken cancelationToken)
    {
        var apiResult = await _authUserService.RegisterAsync(authUserRegister, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthUserResponse>>> Login(AuthUserRegister authUserRegister, CancellationToken cancelationToken)
    {
        var apiResult = await _authUserService.RegisterAsync(authUserRegister, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}
