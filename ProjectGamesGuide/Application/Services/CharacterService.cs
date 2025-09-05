using Core;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Application.Services;

public class CharacterService : IServiceBase<Character>
{
    private readonly IRepositoryBase<Character> _repository;

    public CharacterService(IRepositoryBase<Character> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<Character>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync(cancellationToken);
        return new ApiResponse<IEnumerable<Character>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Ok",
            Data = data
        };
    }
}
