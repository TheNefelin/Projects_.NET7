using Core;
using Microsoft.AspNetCore.Mvc;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuideUserController : ControllerBase
{
    private readonly IServiceByUser<GuideUser> _service;

    public GuideUserController(IServiceByUser<GuideUser> service)
    {
        _service = service;
    }

    [HttpGet("{Id_User}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<GuideUser>>>> GetAllByUserId(Guid Id_User, CancellationToken cancelationToken)
    {
        var apiResult = await _service.GetAllByUserIdAsync(Id_User, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}
