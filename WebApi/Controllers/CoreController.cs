using Core;
using Microsoft.AspNetCore.Mvc;
using ProjectPasswordManager.Application.Interfaces;
using ProjectPasswordManager.Domain.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoreController : ControllerBase
{
    private readonly ICoreService _coreService;

    public CoreController(ICoreService coreService)
    {
        _coreService = coreService;
    }

    [HttpGet("{id_user}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CoreData>>>> GetAllCore(Guid id_user, CancellationToken cancelationToken)
    {
        //Id_User = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
        var apiResult = await _coreService.GetAllAsync(id_user, cancelationToken);
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
    public async Task<ActionResult<ApiResponse<CoreData>>> DeleteCore(int id, Guid idUser, CancellationToken cancelationToken)
    {
        var apiResult = await _coreService.DeleteAsync(id, idUser, cancelationToken);
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}
