using Core;
using Microsoft.AspNetCore.Mvc;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdventureByUserController : ControllerBase
{
    private readonly IServiceByUser<AdventureUser> _service;

    public AdventureByUserController(IServiceByUser<AdventureUser> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<AdventureUser>>>> GetAllByUserId(Guid Id_User, CancellationToken cancelationToken)
    {
        var apiResult = await _service.GetAllByUserIdAsync(Id_User, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}
