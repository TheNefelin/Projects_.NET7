using Core;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Application.Services;

public class GuideUserService : IServiceByUser<GuideUser>
{
    private readonly IRepositoryByUser<GuideUser> _repository;

    public GuideUserService(IRepositoryByUser<GuideUser> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<GuideUser>>> GetAllByUserIdAsync(Guid Id_User, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllByUserIdAsync(Id_User, cancellationToken);
        return new ApiResponse<IEnumerable<GuideUser>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Ok",
            Data = data
        };
    }
}
