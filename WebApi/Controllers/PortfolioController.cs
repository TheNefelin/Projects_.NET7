using Core;
using Microsoft.AspNetCore.Mvc;
using ProjectPortfolio.Application.Interfaces;
using ProjectPortfolio.Domain.Entities;
using System.Threading;

namespace WebApi.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IServicePortfolioBase<Language> _servicePortfolio;

    public PortfolioController(
        IServicePortfolioBase<Language> servicePortfolio)
    {
        _servicePortfolio = servicePortfolio;
    }

    [HttpGet("languages")]
    public async Task<ActionResult<ApiResponse<Language>>> GetLanguages(CancellationToken cancellationToken)
    {
        var result = await _servicePortfolio.GetAllAsync(cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
