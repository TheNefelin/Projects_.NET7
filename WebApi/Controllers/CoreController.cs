using Core;
using Microsoft.AspNetCore.Mvc;
using ProjectPasswordManager.Application.DTOs;
using ProjectPasswordManager.Application.Interfaces;
using ProjectPasswordManager.Domain.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoreController : ControllerBase
{
    private readonly ICoreService _coreService;
    private readonly ICoreUserService _coreUserService;

    public CoreController(ICoreService coreService, ICoreUserService coreUserService)
    {
        _coreService = coreService;
        _coreUserService = coreUserService;
    }

    [HttpPost("register-password")]
    public async Task<ActionResult<ApiResponse<CoreUserIV>>> RegisterCoreUserPassword(CoreUserRequest coreUserRequest, CancellationToken cancelationToken)
    {
        var apiResult = await _coreUserService.RegisterCoreUserPasswordAsync(coreUserRequest, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost("get-iv")]
    public async Task<ActionResult<ApiResponse<CoreUserIV>>> GetCoreUserIV(CoreUserRequest coreUserRequest, CancellationToken cancelationToken)
    {
        var apiResult = await _coreUserService.GetCoreUserIVAsync(coreUserRequest, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpGet("{IdUser}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CoreData>>>> GetAllCore(Guid IdUser, CancellationToken cancelationToken)
    {
        //Id_User = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
        var apiResult = await _coreService.GetAllAsync(IdUser, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CoreData>>> InsertCore(CoreData coreData, CancellationToken cancelationToken)
    {
        var apiResult = await _coreService.InsertAsync(coreData, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<CoreData>>> UpdateCore(CoreData coreData, CancellationToken cancelationToken)
    {
        var apiResult = await _coreService.UpdateAsync(coreData, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<CoreData>>> DeleteCore(CoreDataDelete coreDataDelete, CancellationToken cancelationToken)
    {
        var apiResult = await _coreService.DeleteAsync(coreDataDelete, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}
