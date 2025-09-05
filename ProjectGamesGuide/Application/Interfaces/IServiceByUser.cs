using Core;

namespace ProjectGamesGuide.Application.Interfaces;

public interface IServiceByUser<T>
{
    Task<ApiResponse<IEnumerable<T>>> GetAllByUserIdAsync(Guid Id_User, CancellationToken cancellationToken);
    Task<ApiResponse<object>> UpdateAsync(T userData, CancellationToken cancellationToken);
}
