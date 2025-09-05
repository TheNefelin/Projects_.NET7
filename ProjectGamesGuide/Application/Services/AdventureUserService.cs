using Core;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Application.Services;

public class AdventureUserService : IServiceByUser<AdventureUser>
{
    private readonly IRepositoryByUser<AdventureUser> _repository;

    public AdventureUserService(IRepositoryByUser<AdventureUser> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<AdventureUser>>> GetAllByUserIdAsync(Guid Id_User, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllByUserIdAsync(Id_User, cancellationToken);
        return new ApiResponse<IEnumerable<AdventureUser>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Ok",
            Data = data
        };
    }
}
