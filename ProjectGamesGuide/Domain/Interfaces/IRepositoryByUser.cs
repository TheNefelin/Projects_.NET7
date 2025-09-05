using ProjectGamesGuide.Domain.Entities;

namespace ProjectGamesGuide.Domain.Interfaces;

public interface IRepositoryByUser<T>
{
    Task<IEnumerable<T>> GetAllByUserIdAsync(Guid Id_User, CancellationToken cancellationToken);
    Task<object> UpdateAsync(T userData, CancellationToken cancellationToken);
}
