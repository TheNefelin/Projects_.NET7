using Core;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Application.Services;

public class AdventureService : IServiceBase<Adventure>
{
    private readonly IRepositoryBase<Adventure> _repository;

    public AdventureService(IRepositoryBase<Adventure> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<Adventure>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync(cancellationToken);
        return new ApiResponse<IEnumerable<Adventure>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Ok",
            Data = data
        };
    }
}
