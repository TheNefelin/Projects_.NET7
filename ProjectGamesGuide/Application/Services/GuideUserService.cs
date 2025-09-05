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
        try
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
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<GuideUser>>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }

    public async Task<ApiResponse<object>> UpdateAsync(GuideUser userData, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.UpdateAsync(userData, cancellationToken);
            return new ApiResponse<object>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Ok"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }
}
